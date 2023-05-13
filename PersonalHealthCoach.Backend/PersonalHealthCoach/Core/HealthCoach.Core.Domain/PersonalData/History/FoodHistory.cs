using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class FoodHistory : AggregateRoot
{
    public FoodHistory() { }

    public FoodHistory(Guid userId) : this()
    {
        Id = userId;
        ConsumedFoods = new List<ConsumedFood>();
        UpdatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<FoodHistory> Instance(Guid userId) => Result.Success(new FoodHistory(userId));

    public List<ConsumedFood> ConsumedFoods { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void AddFoods(IReadOnlyCollection<ConsumedFood> foods)
    {
        UpdatedAt = TimeProvider.Instance().UtcNow;
        ConsumedFoods.AddRange(foods);
    }
}

public sealed class ConsumedFood : AggregateRoot
{
    public ConsumedFood() { }

    public ConsumedFood(string title, string meal, int calories, int quantity) : this()
    {
        Title = title;
        Meal = meal;

        Calories = calories;
        Quantity = quantity;

        ConsumedAt = TimeProvider.Instance().UtcNow;
        IsNew = false;
    }

    public static ConsumedFood Create(string title, string meal, int calories, int quantity) => new(title, meal, calories, quantity);

    public string Title { get; private set; }

    public string Meal { get; private set; }

    public DateTime ConsumedAt { get; private set; }

    public int Calories { get; private set; }

    public int Quantity { get; private set; }

    public bool IsNew { get; private set; }

    public void ResetIsNew()
    {
        IsNew = false;
    }
}
