using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using HealthCoach.Core.Domain;
using HealthCoach.Core.Business;
using System.Text;

namespace HealthCoach.Presentation.Tests;


public partial class PersonalDataTests
{
    [Fact]
    public void Given_AddPersonalData_When_RequestIsInvalid_Then_ShouldSendBadResponse()
    {
        //Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var badId = Guid.Empty;

        //Act
        var response = client.PostAsync(string.Format(Routes.PersonalData.GetAllPersonalData, badId) , content).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_AddPersonalData_When_RequestIsValid_Then_ShouldSendSuccessAndPersonalData()
    {
        //Arrange
        var user = MockSetups.SetupUser();

        //Act
        var personalDataCommand = new AddPersonalDataCommand(user.Id, PersonalDataConstants.MinimumDateOfBirth, 80.5f, 195f, new List<string> { "Asthma", "Allergies" },
                new List<string> { "Acne" }, PersonalDataConstants.AllowedGoals.ElementAt(0),
                new List<string> { "Boxing", "Cycling" }, 100, 8, PersonalDataConstants.AllowedGenders.ElementAt(0), true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false);

        var json = JsonConvert.SerializeObject(personalDataCommand);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        var response = client.PostAsync(string.Format(Routes.PersonalData.AddPersonalData, user.Id), content).GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");
    }
}

