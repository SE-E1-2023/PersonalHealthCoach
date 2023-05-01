
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain;

public sealed class DietPlan : AggregateRoot
{

    public DietPlan() { }

    public DietPlan(string name, string scope, List<string> dietType, List<string> recommandations, List<string> interdictions) : this()
    {
        Name = name;
        Scope = scope;
        DietType = dietType;
        Recommandations = recommandations;
        Interdictions = interdictions;
    }

    public static Result<DietPlan> Create(string name, string scope, List<string> dietType, List<string> recommandations, List<string> interdictions)
    {
        return Result.Success().Map(() => new DietPlan(name, scope, dietType, recommandations, interdictions));
    }

    public string Name { get; private set; }

    public string Scope { get; private set; }

    public List<string> DietType { get; private set; }

    public List<string> Recommandations { get; private set; }  

    public List<string> Interdictions { get; private set; }

}
