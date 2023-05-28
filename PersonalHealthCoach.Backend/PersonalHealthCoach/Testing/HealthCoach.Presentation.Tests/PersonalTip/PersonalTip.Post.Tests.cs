using FluentAssertions;
using HealthCoach.Core.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class PersonalTipTests
{
    private readonly HttpClient client = new();

    [Fact]
    public void Given_CreatePersonalTip_When_UserNotFound_Then_ShouldSendBadRequest()
    {
        //Arrange
        var badUserId = Guid.Empty;
        var content = new StringContent("", Encoding.UTF8, "application/json");
        //Act
        var response = client.PostAsync(string.Format(Routes.PersonalTip.CreatePersonalTip, badUserId),content).GetAwaiter()
            .GetResult();
        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_CreatePersonalTip_When_RequestIsValid_Then_ShouldSendSuccess()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();
        var personalData = MockSetups.SetupPersonalData(userMock.Id);

        var command = new CreatePersonalTipCommand(userMock.Id);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = client.PostAsync(string.Format(Routes.PersonalTip.CreatePersonalTip, userMock.Id), content).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

    }
}
