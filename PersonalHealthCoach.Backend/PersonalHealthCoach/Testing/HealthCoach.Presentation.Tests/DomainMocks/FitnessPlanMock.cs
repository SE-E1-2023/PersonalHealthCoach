namespace HealthCoach.Presentation.Tests;

public sealed record FitnessPlanMock(Guid Id, Guid UserId, DateTime Date, List<ExerciseHistoryMock> Exercises);
