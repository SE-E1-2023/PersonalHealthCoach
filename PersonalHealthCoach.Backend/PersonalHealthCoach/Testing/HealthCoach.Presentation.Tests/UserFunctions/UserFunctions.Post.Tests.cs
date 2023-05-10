using System.Text;
using FluentAssertions;
using HealthCoach.Core.Business;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class UserFunctionsTests
{
    private readonly HttpClient client = new();

    [Fact]
    public void Given_CreateUser_When_RequestIsInvalid_Then_ShouldSendBadResponse()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");

        // Act
        var response = client.PostAsync(Routes.User.CreateUser, content).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_CreateUser_When_RequestIsValid_Then_ShouldSendSuccessAndUserData()
    {
        //Arrange
        var guidPrefix = Guid.NewGuid().ToString().Substring(0, 8);
        var randomEmail = $"{guidPrefix}@EMAIL.com";

        var command = new CreateUserCommand("Name", "FirstName", randomEmail);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = client.PostAsync(Routes.User.CreateUser, content).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var user = JsonConvert.DeserializeObject<UserMock>(responseBody);
        user.Should().NotBeNull();
        user.Name.Should().Be("Name");
        user.FirstName.Should().Be("FirstName");
        user.EmailAddress.Should().Be(randomEmail);
    }
    
    private record UserMock(Guid Id, string Name, string FirstName, string EmailAddress, bool HasElevatedRights);
}