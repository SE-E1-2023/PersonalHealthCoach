namespace HealthCoach.Core.Domain.Tests;

public static class FoodHistoryFactory
{
    public static FoodHistory WithId(Guid userId) => FoodHistory.Instance(userId).Value;

    public static FoodHistory Any() => WithId(Guid.NewGuid());
}