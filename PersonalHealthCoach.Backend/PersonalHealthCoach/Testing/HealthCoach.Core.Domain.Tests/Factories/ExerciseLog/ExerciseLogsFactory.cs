namespace HealthCoach.Core.Domain.Tests;

public static class ExerciseLogsFactory
{
    public static ExerciseLog WithId(Guid userId) => ExerciseLog.Instance(userId).Value;

    public static ExerciseLog Any() => WithId(Guid.NewGuid());
}