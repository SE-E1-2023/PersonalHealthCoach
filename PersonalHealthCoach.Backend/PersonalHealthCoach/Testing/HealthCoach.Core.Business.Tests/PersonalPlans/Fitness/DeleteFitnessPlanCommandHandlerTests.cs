using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class DeleteFitnessPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_FitnessPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = new DeleteFitnessPlanCommand(Guid.NewGuid());
        
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Get.FitnessPlanNotFound);

        repositoryMock.Verify(r => r.Delete(It.IsAny<FitnessPlan>()), Times.Never);
    }

    [Fact]
    public void When_FitnessPlanExists_Then_ShouldDelete()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var exerciseCount = fitnessPlan.Exercises.Count;

        var command = Command() with { FitnessPlanId = fitnessPlan.Id };
        
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan> { fitnessPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        
        repositoryMock.Verify(x => x.Delete(It.IsAny<FitnessPlan>()), Times.Once);
        repositoryMock.Verify(x => x.Delete(It.IsAny<Domain.Exercise>()), Times.Exactly(exerciseCount));
    }

    private DeleteFitnessPlanCommand Command() => new(Guid.NewGuid());

    private DeleteFitnessPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}
