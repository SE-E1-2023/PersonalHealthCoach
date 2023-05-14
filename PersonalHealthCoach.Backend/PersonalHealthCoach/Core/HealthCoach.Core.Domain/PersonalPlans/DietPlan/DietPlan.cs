using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class DietPlan : AggregateRoot
{

    public DietPlan() { }

    private DietPlan(Guid userId, string name, string scope, List<string> dietType, List<string> recommendations, List<string> interdictions, Meal breakfast, Meal drink, Meal mainCourse, Meal sideDish, Meal snack, Meal soup) : this()
    {
        UserId = userId;
        Name = name;
        Scope = scope;
        DietType = dietType;
        Recommendations = recommendations;
        Interdictions = interdictions;
        MainCourse = mainCourse;
        SideDish = sideDish;
        Snack = snack;
        Soup = soup;
        Breakfast = breakfast;
        Drink = drink;
        CreatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<DietPlan> Create(Guid userId, string name, string scope, List<string> dietType, List<string> recommandations, List<string> interdictions, Meal breakfast, Meal drink, Meal mainCourse, Meal sideDish, Meal snack, Meal soup)
    {
        return Result.Success().Map(() => new DietPlan(userId, name, scope, dietType, recommandations, interdictions, breakfast, drink, mainCourse, sideDish, snack, soup));
    }

    public Guid UserId { get; private set; }

    public string Name { get; private set; }

    public string Scope { get; private set; }

    public List<string> DietType { get; private set; }

    public List<string> Recommendations { get; private set; }  

    public List<string> Interdictions { get; private set; }

    public Meal Breakfast { get; private set; }

    public Meal Drink { get; private set; }

    public Meal MainCourse { get; private set; }

    public Meal SideDish { get; private set; }

    public Meal Snack { get; private set; }

    public Meal Soup { get; private set; }

    public DateTime CreatedAt { get; private set; }
}

public sealed class Meal : AggregateRoot
{
    public Meal() { }

    public Meal(string imageUrl, IReadOnlyCollection<string> ingredients, double calories, string title) : this()
    {
        ImageUrl = imageUrl;
        Ingredients = ingredients.ToList();
        Calories = calories;
        Title = title;
    }

    public string ImageUrl { get; private set; }

    public List<string> Ingredients { get; private set; }

    public double Calories { get; private set; }

    public string Title { get; private set; }
}

