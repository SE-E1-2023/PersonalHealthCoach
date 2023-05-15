using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class FoodFunctionsTests
{

    private readonly HttpClient client = new();

    [Fact]
    public void Given_GetFoodHistory_When_UserNotFound_Then_ShouldSendBadRequest()
    {
        //Arrange
        var badUserId = Guid.Empty;
        //Act
        var response = client.GetAsync(string.Format(Routes.FoodHistory.GetFoodHistory, badUserId)).GetAwaiter()
            .GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_GetFoodHistory_When_UserFound_Then_ShouldSendSuccessAndFoodHistory()
    {
        // Arrange
        var user = MockSetups.SetupUser();
        var foodHistory = MockSetups.SetupFoodHistory(user.Id);
        // Act
        var response = client.GetAsync(string.Format(Routes.FoodHistory.GetFoodHistory, user.Id)).GetAwaiter()
            .GetResult();
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var foodHistoryResponse = JsonConvert.DeserializeObject<FoodHistoryMock>(responseBody);
        foodHistoryResponse.Should().NotBeNull();
    }
    private record FoodHistoryMock(Guid Id, Guid UserId, Guid FoodId, DateTime Date, int Quantity);
}
