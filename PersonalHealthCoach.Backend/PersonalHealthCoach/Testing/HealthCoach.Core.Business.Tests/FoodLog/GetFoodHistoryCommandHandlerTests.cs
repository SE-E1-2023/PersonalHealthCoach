using Moq;
using Xunit;
using FluentAssertions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Core.Domain.Tests;

namespace HealthCoach.Core.Business.Tests;
public class GetFoodHistoryCommandHandlerTest
{
    private readonly Mock<IEfQueryProvider> queryProviderMock = new();

    [Fact]
    public void When_LogIsEmpty_Then_ShouldFail()
    {
        //Arrange

        var command = new GetFoodHistoryCommand(Guid.NewGuid());
        queryProviderMock
            .Setup(x => x.Query<FoodHistory>())
            .Returns(new List<FoodHistory>().AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BusinessErrors.FoodHistory.Get.FoodHistoryNotFound);
    }

    [Fact]
    public void When_FoodHistoryDoesExist_Then_ShouldSucceed()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var history = FoodHistoryFactory.WithId(userId);
        var command = new GetFoodHistoryCommand(userId);
        queryProviderMock
            .Setup(x => x.Query<FoodHistory>())
            .Returns(new List<FoodHistory> { history }.AsQueryable());

        //Act
        var result = Sut().Handle(command, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().Be(history);
    }

    private GetFoodHistoryCommandHandler Sut() => new(
        queryProviderMock.Object
    );
}