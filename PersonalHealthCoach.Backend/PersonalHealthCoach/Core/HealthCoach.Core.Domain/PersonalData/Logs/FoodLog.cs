using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class FoodLog : AggregateRoot
{
    public FoodLog() { }

    public FoodLog(Guid userId) : this()
    {
        Id = userId;
        ConsumedFoods = new List<ConsumedFood>();
        UpdatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<FoodLog> Instance(Guid userId) => Result.Success(new FoodLog(userId));

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

    public ConsumedFood(string title) : this()
    {
        Title = title;
        ConsumedAt = TimeProvider.Instance().UtcNow;
        IsNew = false;
    }

    public static ConsumedFood Create(string title) => new(title);

    public string Title { get; private set; }

    public DateTime ConsumedAt { get; private set; }
    public bool IsNew { get; private set; }

    public void ResetIsNew()
    {
        IsNew = false;
    }
}
