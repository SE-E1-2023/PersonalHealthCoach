using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class FitnessPlan
{
    //private readonly HttpClient client = new();

    [Fact]
    public void Given_GetFitnessPlan_When_UserNotFound_Then_ShouldSendBadRequest()
    {
        //Arrange
        var badUserId = Guid.Empty;
        //Act
        var response = client.GetAsync(string.Format(Routes.FitnessPlan.GetFitnessPlan, badUserId)).GetAwaiter()
            .GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_GetFitnessPlan_When_UserFound_Then_ShouldSendSuccessAndFitnessPlan()
    {
        // Arrange
        var user = MockSetups.SetupUser();
        var fitnessPlan = MockSetups.SetupFitnessPlan(user.Id);
        // Act
        var response = client.GetAsync(string.Format(Routes.FitnessPlan.GetFitnessPlan, user.Id)).GetAwaiter()
            .GetResult();
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var fitnessPlanResponse = JsonConvert.DeserializeObject<FitnessPlanMock>(responseBody);
        fitnessPlanResponse.Should().NotBeNull();
    }
}