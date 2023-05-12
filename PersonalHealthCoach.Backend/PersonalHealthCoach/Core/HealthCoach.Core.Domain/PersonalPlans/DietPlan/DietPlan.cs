using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class DietPlan : AggregateRoot
{

    public DietPlan() { }

    private DietPlan(string name, string scope, List<string> dietType, List<string> recommendations, List<string> interdictions) : this()
    {
        Name = name;
        Scope = scope;
        DietType = dietType;
        Recommendations = recommendations;
        Interdictions = interdictions;
    }

    public static Result<DietPlan> Create(string name, string scope, List<string> dietType, List<string> recommandations, List<string> interdictions)
    {
        return Result.Success().Map(() => new DietPlan(name, scope, dietType, recommandations, interdictions));
    }

    public string Name { get; private set; }

    public string Scope { get; private set; }

    public List<string> DietType { get; private set; }

    public List<string> Recommendations { get; private set; }  

    public List<string> Interdictions { get; private set; }
}