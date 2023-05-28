using FluentAssertions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class FitnessPlan
{
    //private readonly HttpClient client = new();

    [Fact]
    public void Given_GetLatestFitnessPlan_When_UserDoesNotExist_Then_ShouldSendBadResponse()
    {
        // Arrange
        var badGuid = Guid.Empty;
        // Act
        var response = client.GetAsync(string.Format(Routes.FitnessPlan.GetLatestFitnessPlan, badGuid)).GetAwaiter().GetResult();
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_GetFitnessPlan_When_UserFoundAndNoFitnessPlan_Then_ShouldSend_BadRequest()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();
        //Act
        var response = client.GetAsync(string.Format(Routes.FitnessPlan.GetLatestFitnessPlan, userMock.Id)).GetAwaiter().GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");

    }

    [Fact]
    public void Given_GetFitnessPlan_When_UserFoundAndHasFitnessPlan_Then_ShouldSendSuccessAndFitnessPlan()
    {
        // Arrange
        var user = MockSetups.SetupUser();
        var fitnessPlan = MockSetups.SetupFitnessPlan(user.Id);
        // Act
        var response = client.GetAsync(string.Format(Routes.FitnessPlan.GetLatestFitnessPlan, user.Id)).GetAwaiter()
            .GetResult();
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var fitnessPlanResponse = JsonConvert.DeserializeObject<FitnessPlanMock>(responseBody);
        fitnessPlanResponse.Should().NotBeNull();
    }
}