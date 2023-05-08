using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;
public sealed class UpdateFoodLogCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IFoodLogRepository> foodLogRepositoryMock = new();

    [Fact]
    public void When_UserNotFound_Then_ShouldFail()
    {
        //Arrange
        var command = Command();

        repositoryMock
            .Setup(x => x.Load<User>(command.UserId))
            .ReturnsAsync(Maybe.None);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FoodLog.AddFoods.UserNotFound);
    }

    [Fact]
    public void When_DomainSucceeds_Then_ShouldSucceed()
    {
        //Arrange
        var user = UsersFactory.Any();
        var command = Command() with { UserId = user.Id };

        repositoryMock.Setup(x => x.Load<User>(command.UserId)).ReturnsAsync(user);

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();

        foodLogRepositoryMock.Verify(r => r.Store(user.Id, command.Foods), Times.Once);
    }

    private UpdateFoodLogCommand Command() => new(Guid.NewGuid(), new List<string> { "Food 1", "Food 2" });

    private UpdateFoodLogCommandHandler Sut() => new(repositoryMock.Object, foodLogRepositoryMock.Object);
}