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
    public void Given_CreateFitnessPlan_When_UserDoesNotExist_Then_ShouldSendBadResponse()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var badGuid = Guid.Empty;
        // Act
        var response = client.PostAsync(string.Format(Routes.FitnessPlan.CreateFitnessPlan, badGuid), content).GetAwaiter().GetResult();
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_CreateFitnessPlan_When_UserExistsAndNoPersonalData_Then_ShouldSendBadRequest()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();
        var command = new CreateFitnessPlanCommand(userMock.Id, 5);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = client.PostAsync(string.Format(Routes.FitnessPlan.CreateFitnessPlan, userMock.Id), content).GetAwaiter().GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_CreateFitnessPlan_When_ValidRequest_Then_ShouldSendSuccessAndFitnessPlan()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();
        MockSetups.SetupPersonalData(userMock.Id);
        var command = new CreateFitnessPlanCommand(userMock.Id, 5);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = client.PostAsync(string.Format(Routes.FitnessPlan.CreateFitnessPlan, userMock.Id), content).GetAwaiter().GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var fitnessPlan = JsonConvert.DeserializeObject<FitnessPlanMock>(responsebody);
        fitnessPlan.Should().NotBeNull();
    }
    private record FitnessPlanMock(Guid Id, Guid UserId, int Days);
}

