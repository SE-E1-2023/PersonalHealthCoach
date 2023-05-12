using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain.Tests;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business.Tests;
public sealed class UpdateFoodHistoryCommandHandlerTests
{
    private readonly Mock<IRepository> repositoryMock = new();
    private readonly Mock<IFoodHistoryRepository> foodHistoryRepositoryMock = new();

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
        result.Error.Should().Be(BusinessErrors.FoodHistory.AddFoods.UserNotFound);
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

        foodHistoryRepositoryMock.Verify(r => r.Store(user.Id, command.Foods), Times.Once);
    }

    private UpdateFoodHistory Command() => new(Guid.NewGuid(), new List<Food>
        {
            new("Food no. 1", 100, 1),
            new("Food no. 2", 101, 2),
            new("Food no. 3", 102, 1),
            new("Food no. 4", 1001, 2)
        }
    );

    private UpdateFoodHistoryCommandHandler Sut() => new(repositoryMock.Object, foodHistoryRepositoryMock.Object);
}