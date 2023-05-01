using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class DeleteDietPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_DietPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = new DeleteDietPlanCommand(Guid.NewGuid());

        queryProviderMock.Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Delete.DietPlanDoesNotExist);

        repositoryMock.Verify(r => r.Delete(It.IsAny<DietPlan>()), Times.Never);
    }

    [Fact]
    public void When_DietPlanExists_Then_ShouldDelete()
    {
        //Arrange
        var dietPlan = PlansFactory.DietPlans.Any();

        var command = Command() with { DietPlanId = dietPlan.Id };

        queryProviderMock.Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan> { dietPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(x => x.Delete(It.IsAny<DietPlan>()), Times.Once);
    }

    private DeleteDietPlanCommand Command() => new(Guid.NewGuid());

    private DeleteDietPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}
