using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using HealthCoach.Core.Domain;
namespace HealthCoach.Presentation.Tests;

public partial class ExerciseHistoryTests
{
    [Fact]
    public void Given_GetExerciseHistory_When_UserNotFound_Then_ShouldSendBadRequest()
    {
        //Arrange
        Guid badGuid = Guid.NewGuid();

        //Act
        var response = client.GetAsync(string.Format(Routes.ExerciseHistory.GetExerciseHistory, badGuid)).GetAwaiter().GetResult();

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void GivenGetExerciseHistory_When_UserFoundAndExerciseHistoryNotFound_Then_ShouldSendBadRequest()
    {
        // Arrange
        var user = MockSetups.SetupUser();

        // Act

        var response = client.GetAsync(string.Format(Routes.ExerciseHistory.GetExerciseHistory, user.Id)).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");

    }

    [Fact]
    public void GivenGetExerciseHistory_When_UserFoundAndExerciseHistoryFound_Then_ShouldSendSuccessAndExerciseHistory()
    {
        // Arrange
        var user = MockSetups.SetupUser();
        var exerciseHistoryMock = MockSetups.SetupExerciseMistory(user.Id);


        // Act
        var response = client.GetAsync(string.Format(Routes.ExerciseHistory.GetExerciseHistory, user.Id)).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("OK");

        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var exerciseHistory = JsonConvert.DeserializeObject<ExerciseHistoryMock>(responseBody);
        exerciseHistory.Should().NotBeNull();

    }
}