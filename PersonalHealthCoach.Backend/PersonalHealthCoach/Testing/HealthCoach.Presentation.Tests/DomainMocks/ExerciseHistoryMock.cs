using HealthCoach.Core.Business;
namespace HealthCoach.Presentation.Tests;

public sealed record ExerciseHistoryMock(Guid UserId, IReadOnlyCollection<Exercise> CompletedExercises, DateTime UpdateAt);