namespace HealthCoach.Presentation.Tests;

public sealed record FoodHistoryMock(Guid Id, DateTime UpdatedAt, List<ConsumedFoodMock> ConsumedFoods);

public sealed record ConsumedFoodMock(Guid Id, string Title, string Meal, int Calories, DateTime ConsumedAt,
    int Quantity, bool IsNew);
