using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Business;

namespace HealthCoach.Presentation.Tests;

public partial class PersonalDataTests
{
    private readonly HttpClient client = new();
  
    [Fact]
    public void Given_GetAllPersonalData_When_UserNotFound_Then_ShouldSendBadRequest()
    {
        //Arrange
        Guid badGuid = Guid.NewGuid();

        //Act
        var response = client.GetAsync(string.Format(Routes.PersonalData.GetAllPersonalData, badGuid)).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_GetAllPersonalData_When_UserIsFoundButNoPersonalDataAssociated_Then_ShouldSendBadRequest()
    {
        //Arrange
        var user = MockSetups.SetupUser();

        //Act
        var response = client.GetAsync(string.Format(Routes.PersonalData.GetAllPersonalData, user.Id)).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_GetAllPersonalData_When_UserIsFoundAndHasPersonalDataAssociated_Then_ShouldSendSuccessAndPersonalData()
    {
        //Arrange
        var user = MockSetups.SetupUser();
        MockSetups.SetupPersonalData(user.Id);

        //Act
        var response = client.GetAsync(string.Format(Routes.PersonalData.GetAllPersonalData, user.Id)).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");
    }

    public void Given_GetLatestPersonalData_When_UserFoundAndPersonalDataNotFound_Then_ShouldSendBadRequest()
    {
        // Arrange
        var user = MockSetups.SetupUser();

        // Act

        var response = client.GetAsync(string.Format(Routes.PersonalData.RetrieveLatestPersonalData, user.Id)).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");

    }

    [Fact]
    public void Given_GetLatestPersonalData_When_UserFoundAndPersonalDataFound_Then_ShouldSendSuccessAndPersonalData()
    {
        // Arrange
        var user = MockSetups.SetupUser();
        var personalDataMock = MockSetups.SetupPersonalData(user.Id);


        // Act
        var response = client.GetAsync(string.Format(Routes.PersonalData.RetrieveLatestPersonalData, user.Id)).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var personalData= JsonConvert.DeserializeObject<PersonalDataMock>(responseBody);
        personalData.Should().NotBeNull();

    }
}
