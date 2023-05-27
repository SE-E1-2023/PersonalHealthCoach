﻿using FluentAssertions;
using Newtonsoft.Json;
using Xunit;


namespace HealthCoach.Presentation.Tests;

public partial class DietPlanTests
{
    private readonly HttpClient client = new();

    [Fact]
    public void Given_CreateDietPlan_When_UserDoesNotExist_Then_ShouldSendBadResponse()
    {
        //Arrange
        var badId = Guid.Empty;

        //Act
        var response = client.PostAsync(string.Format(Routes.DietPlan.CreateDietPlan, badId), null).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_CreateDietPlan_When_UserExists_Then_ShouldSendSuccessAndDietPlanData()
    {
        //Arrange
        var user = MockSetups.SetupUser();

        //Act
        var response = client.PostAsync(string.Format(Routes.DietPlan.CreateDietPlan, user.Id), null).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var dietPlan = JsonConvert.DeserializeObject<DietPlanMock>(responseBody);
        dietPlan.Should().NotBeNull();
    }
}