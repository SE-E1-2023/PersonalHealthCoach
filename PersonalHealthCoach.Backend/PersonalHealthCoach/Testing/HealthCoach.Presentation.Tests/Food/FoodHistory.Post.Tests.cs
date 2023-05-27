using System.Text;
using FluentAssertions;
using HealthCoach.Core.Business;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class FoodFunctionsTests
{

    [Fact]
    public void Given_UpdateFoodHistory_When_RequestIsInvalid_Then_ShouldSendBadResponse()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var badGuid = Guid.Empty;
        // Act
        var response = client.PostAsync(string.Format(Routes.FoodHistory.UpdateFoodHistory, badGuid), content).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_UpdateFoodHistory_When_RequestIsValid_Then_ShouldSendSuccessAndNoContent()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();
        var foodHistoryMock = MockSetups.SetupFoodHistory(userMock.Id);

        var food = new Food("Lunch", "Pizza", 100, 1);
        IReadOnlyCollection<Food> newFood = new List<Food>() { food };
        var command = new UpdateFoodHistoryCommand(userMock.Id, newFood);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = client.PostAsync(string.Format(Routes.FoodHistory.UpdateFoodHistory, userMock.Id), content).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("No Content");

    }
}