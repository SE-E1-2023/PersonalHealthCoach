using CSharpFunctionalExtensions;
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
    private readonly User manager = UsersFactory.AnyManager();

    [Fact]
    public void When_UserIsNotFound_Then_ShouldFail()
    {
        //Arrange
        var dietPlan = PlansFactory.DietPlans.Any();
        var command = new DeleteDietPlanCommand(dietPlan.Id, Guid.NewGuid());

        repositoryMock.Setup(x => x.Load<User>(command.CallerId)).ReturnsAsync(Maybe<User>.None);
        queryProviderMock.Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan> { dietPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Delete.UserNotFound);

        repositoryMock.Verify(r => r.Delete(It.IsAny<DietPlan>()), Times.Never);
    }

    [Fact]
    public void When_UserIsNotAuthorized_Then_ShouldFail()
    {
        //Arrange
        var user = UsersFactory.Any();
        var dietPlan = PlansFactory.DietPlans.Any();
        var command = Command() with { CallerId = user.Id, DietPlanId = dietPlan.Id };

        repositoryMock.Setup(x => x.Load<User>(command.CallerId)).ReturnsAsync(user);
        queryProviderMock.Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan> { dietPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.DietPlan.Delete.UserNotAuthorized);

        repositoryMock.Verify(r => r.Delete(It.IsAny<DietPlan>()), Times.Never);
    }

    [Fact]
    public void When_DietPlanDoesNotExist_Then_ShouldFail()
    {
        //Arrange
        var command = new DeleteDietPlanCommand(Guid.NewGuid(), manager.Id);

        repositoryMock.Setup(x => x.Load<User>(manager.Id)).ReturnsAsync(manager);
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

        repositoryMock.Setup(x => x.Load<User>(manager.Id)).ReturnsAsync(manager);
        queryProviderMock.Setup(x => x.Query<DietPlan>()).Returns(new List<DietPlan> { dietPlan }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        repositoryMock.Verify(x => x.Delete(It.IsAny<DietPlan>()), Times.Once);
    }

    private DeleteDietPlanCommand Command() => new(Guid.NewGuid(), manager.Id);

    private DeleteDietPlanCommandHandler Sut() => new(repositoryMock.Object, queryProviderMock.Object);
}
