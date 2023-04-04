using Xunit;
using FluentAssertions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain.Tests;

public partial class FitnessPlanTests
{
    [Theory, MemberData(nameof(ExerciseListIsNullOrEmptyData))]
    public void Given_Create_When_ExerciseListIsNullOrEmpty_Then_ShouldFail(List<Exercise> badList)
    {
        //Act
        var result = FitnessPlan.Create(Guid.NewGuid(), badList);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FitnessPlan.Create.NoExercises);
    }

    [Fact]
    public void Given_Create_When_NotViolatingConstraints_Then_ShouldSucceed()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var exercises = PlansFactory.Exercises.Any();
        var now = TimeProviderContext.AdvanceTimeToNow();

        //Act
        var result = FitnessPlan.Create(userId, exercises);

        //Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.UserId.Should().Be(userId);
        result.Value.Exercises.Should().BeEquivalentTo(exercises);
        result.Value.CreatedAt.Should().Be(now);
    }

    public static IEnumerable<object[]> ExerciseListIsNullOrEmptyData =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { new List<Exercise>() }
        };
}