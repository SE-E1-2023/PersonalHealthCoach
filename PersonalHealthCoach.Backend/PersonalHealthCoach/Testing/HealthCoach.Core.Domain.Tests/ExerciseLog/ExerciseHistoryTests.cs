using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain.Tests;

public sealed class ExerciseHistoryTests
{
    [Fact]
    public void Given_Instance_Then_ShouldCreateInstance()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var result = ExerciseHistory.Instance(id);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.CompletedExercises.Should().BeEmpty();
    }

    [Fact]
    public void Given_AddExercises_Then_ShouldAddExercises()
    {
        //Arrange
        var exerciseList = new List<CompletedExercise>
        {
            CompletedExercise.Create("Exercise 1", 100, 1),
            CompletedExercise.Create("Exercise 2", 100, 1),
            CompletedExercise.Create("Exercise 3", 100, 1)
        };
        var exerciseLog = ExerciseHistoryFactory.Any();
        var now = TimeProviderContext.AdvanceTimeToNow();

        //Act
        exerciseLog.AddExercises(exerciseList);

        //Assert
        exerciseLog.CompletedExercises.Should().HaveCount(3);
        exerciseLog.CompletedExercises.Should().BeEquivalentTo(exerciseList);
    }
}