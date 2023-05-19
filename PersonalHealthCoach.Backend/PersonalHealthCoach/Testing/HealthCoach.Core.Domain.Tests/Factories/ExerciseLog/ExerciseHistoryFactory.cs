namespace HealthCoach.Core.Domain.Tests;

public static class ExerciseHistoryFactory
{
    public static ExerciseHistory WithId(Guid userId) => ExerciseHistory.Instance(userId).Value;

    public static ExerciseHistory Any() => WithId(Guid.NewGuid());
}