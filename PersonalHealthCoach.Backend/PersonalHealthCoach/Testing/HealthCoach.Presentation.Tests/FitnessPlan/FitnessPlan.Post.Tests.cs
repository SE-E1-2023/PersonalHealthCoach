using System.Text;
using FluentAssertions;
using HealthCoach.Core.Business;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class FitnessPlan
{
    private readonly HttpClient client = new();

    [Fact]
    public void Given_UpdateFitnessPlan_When_RequestIsInvalid_Then_ShouldSendBadResponse()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var badGuid = Guid.Empty;
        // Act
        var response = client.PostAsync(string.Format(Routes.FitnessPlan.UpdateFitnessPlan, badGuid), content).GetAwaiter().GetResult();
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_UpdateFoodHistory_When_RequestIsValid_Then_ShouldSendSuccessAndNoContent()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();
        var fitnessPlanMock = MockSetups.SetupFitnessPlan(userMock.Id);
        var command = new CreateFitnessPlanCommand(userMock.Id, 5);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = client.PostAsync(string.Format(Routes.FitnessPlan.UpdateFitnessPlan, userMock.Id), content).GetAwaiter().GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("No Content");
    }
    private record FitnessPlanMock(Guid Id, Guid UserId, int Days);
}

