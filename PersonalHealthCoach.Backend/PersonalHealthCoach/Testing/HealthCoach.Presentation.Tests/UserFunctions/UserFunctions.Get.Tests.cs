using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class UserFunctionsTests
{
    [Fact]
    public void Given_GetUserByEmailAddress_When_UserNotFound_Then_ShouldSendBadRequest()
    {
        //Arrange
        var badEmail = "!!!!!!!!!!!!!!!!";

        //Act
        var response = client.GetAsync(string.Format(Routes.User.GetUserByEmailAddress, badEmail)).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_GetUserByEmailAddress_When_UserFound_Then_ShouldSendSuccessAndUserId()
    {
        // Arrange
        var user = MockSetups.SetupUser();

        // Act
        var response = client.GetAsync(string.Format(Routes.User.GetUserByEmailAddress, user.EmailAddress)).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var userId = JsonConvert.DeserializeObject<Guid>(responseBody);
        userId.Should().Be(user.Id);
    }
}