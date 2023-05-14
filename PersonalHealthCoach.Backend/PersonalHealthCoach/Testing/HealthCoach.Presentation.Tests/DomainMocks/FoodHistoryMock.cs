namespace HealthCoach.Presentation.Tests;

public sealed record FoodHistoryMock(Guid Id, Guid UserId, Guid FoodId, DateTime Date, int Quantity)
{
    public ConsumedFoodMock Food { get; init; }
}

public sealed record ConsumedFoodMock(Guid Id, string Title, string Meal, int Calories)
{
    public static ConsumedFoodMock Create(Guid id, string title, string meal, int calories)
    {
        return new ConsumedFoodMock(id, title, meal, calories);
    }
}