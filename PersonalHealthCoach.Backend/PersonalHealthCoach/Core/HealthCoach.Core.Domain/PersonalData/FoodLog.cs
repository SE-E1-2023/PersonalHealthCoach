using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class FoodLog : AggregateRoot
{
    public FoodLog() { }

    public FoodLog(Guid userId) : this()
    {
        Id = userId;
    }

    public static Result<FoodLog> Instance(Guid userId) => Result.Success(new FoodLog(userId));

    public List <ConsumedFood> ConsumedFoods { get; private set; }

    public void AddFoods(IReadOnlyCollection<string> foods)
    {
        foreach (var food in foods)
        {
            var foodResult = food.EnsureNotNullOrEmpty("");
            if (foodResult.IsFailure)
            {
                continue;
            }

            ConsumedFoods.Add(ConsumedFood.Create(food));
        }
    }
}

public sealed class ConsumedFood : AggregateRoot
{
    public ConsumedFood() { }

    public ConsumedFood(string title) : this()
    {
        Title = title;
        ConsumedAt = TimeProvider.Instance().UtcNow;

    }

    public static ConsumedFood Create(string title) => new(title);

    public string Title { get; private set; }

    public DateTime ConsumedAt { get; private set; }
}
