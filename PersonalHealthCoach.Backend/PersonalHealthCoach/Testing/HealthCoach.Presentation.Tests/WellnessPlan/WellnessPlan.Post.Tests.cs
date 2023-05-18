using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class WellnessPlanTests
{
    private readonly HttpClient client = new();

    [Fact]
    public void Given_CreateWellnessPlan_When_UserDoesNotExist_Then_ShouldSendBadResponse()
    {
        //Arrange
        var badId = Guid.Empty;

        //Act
        var response = client.PostAsync(string.Format(Routes.WellnessPlan.CreateWellnessPlan, badId), null).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_CreateWellnessPlan_When_UserExists_Then_ShouldSendSuccessAndWellnessPlanData()
    {
        //Arrange
        var user = MockSetups.SetupUser();

        //Act
        var response = client.PostAsync(string.Format(Routes.WellnessPlan.CreateWellnessPlan, user.Id), null).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var wellnessPlan = JsonConvert.DeserializeObject<WellnessPlanMock>(responseBody);
        wellnessPlan.Should().NotBeNull();
    }
}