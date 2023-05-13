using System.Text;
using FluentAssertions;
using HealthCoach.Core.Business;
using Newtonsoft.Json;
using Xunit;

namespace HealthCoach.Presentation.Tests;

public partial class ExerciseHistoryTests
{
    private readonly HttpClient client = new();

    [Fact]
    public void Given_UpdateExerciseHistory_When_RequestIsInvalid_Then_ShouldSendBadResponse()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var badId = Guid.Empty;
        // Act
        var response = client.PostAsync(string.Format(Routes.ExerciseHistory.UpdateExerciseHistory,badId), content).GetAwaiter().GetResult();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Fact]
    public void Given_UpdateExerciseHistory_When_RequestIsValid_Then_ShouldSendSuccessAndNoContent()
    {
        //Arrange
        var userMock = MockSetups.SetupUser();

        var completedExercise = new Exercise("exercise 1", 100, 10);
        IReadOnlyCollection<Exercise> randomExercises = new List<Exercise>() { completedExercise };
        var command = new UpdateExerciseHistoryCommand(userMock.Id, randomExercises);
        var json = JsonConvert.SerializeObject(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = client.PostAsync(string.Format(Routes.ExerciseHistory.UpdateExerciseHistory,userMock.Id), content).GetAwaiter().GetResult();
        
        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.ReasonPhrase.Should().Be("No Content");

    }

    private record ExerciseHistoryMock(Guid Id, IReadOnlyCollection<Exercise> CompletedExercises, DateTime UpdateAt);
}