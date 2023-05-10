using System.Text;
using FluentAssertions;
using HealthCoach.Core.Business;
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
        var user = SetupUser();

        // Act
        var response = client.GetAsync(string.Format(Routes.User.GetUserByEmailAddress, user.EmailAddress)).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var userId = JsonConvert.DeserializeObject<Guid>(responseBody);
        userId.Should().Be(user.Id);
    }

    private UserMock SetupUser()
    {
        var guidPrefix = Guid.NewGuid().ToString().Substring(0, 8);
        var randomEmail = $"{guidPrefix}@EMAIL.com";
        
        var createUserCommand = new CreateUserCommand("Name", "FirstName", randomEmail);
        var json = JsonConvert.SerializeObject(createUserCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(Routes.User.CreateUser, content).GetAwaiter().GetResult();

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var user = JsonConvert.DeserializeObject<UserMock>(responseBody);

        return user;
    }
}