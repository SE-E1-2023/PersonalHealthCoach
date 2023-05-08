namespace HealthCoach.Core.Domain.Tests;

public static class FoodLogsFactory
{
    public static FoodLog WithId(Guid userId) => FoodLog.Instance(userId).Value;

    public static FoodLog Any() => WithId(Guid.NewGuid());
}