using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;

public class DeleteFitnessPlanCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();
    private readonly User manager = UsersFactory.AnyManager();

    [Fact]
    public void When_FitnessPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = Command() with { CallerId = manager.Id };

        repositoryMock.Setup(x => x.Load<User>(manager.Id)).ReturnsAsync(manager);
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Delete.FitnessPlanNotFound);

        repositoryMock.Verify(r => r.Delete(It.IsAny<FitnessPlan>()), Times.Never);
        repositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Never);
    }

    [Fact]
    public void When_UserIsNotFound_Then_ShouldFail()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var command = Command() with { FitnessPlanId = fitnessPlan.Id };

        repositoryMock.Setup(x => x.Load<User>(command.CallerId)).ReturnsAsync(Maybe<User>.None);
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan> { fitnessPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Delete.UserNotFound);

        repositoryMock.Verify(r => r.Delete(It.IsAny<FitnessPlan>()), Times.Never);
        repositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Never);
    }

    [Fact]
    public void When_UserIsNotAuthorized_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var command = Command() with { CallerId = manager.Id, FitnessPlanId = fitnessPlan.Id };

        repositoryMock.Setup(x => x.Load<User>(command.CallerId)).ReturnsAsync(user);
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan> { fitnessPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FitnessPlan.Delete.UserNotAuthorized);

        repositoryMock.Verify(r => r.Delete(It.IsAny<FitnessPlan>()), Times.Never);
        repositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Never);
    }

    [Fact]
    public void When_FitnessPlanExists_Then_ShouldDelete()
    {
        //Arrange
        var fitnessPlan = PlansFactory.FitnessPlans.Any();
        var exerciseCount = fitnessPlan.Exercises.Count;

        var command = Command() with { FitnessPlanId = fitnessPlan.Id, CallerId = manager.Id };

        repositoryMock.Setup(x => x.Load<User>(manager.Id)).ReturnsAsync(manager);
        queryProviderMock.Setup(x => x.Query<FitnessPlan>()).Returns(new List<FitnessPlan> { fitnessPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        
        repositoryMock.Verify(x => x.Delete(It.IsAny<FitnessPlan>()), Times.Once);
        repositoryMock.Verify(x => x.Delete(It.IsAny<Exercise>()), Times.Exactly(exerciseCount));
    }

    private DeleteFitnessPlanCommand Command() => new(Guid.NewGuid(), manager.Id);

    private DeleteFitnessPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}
