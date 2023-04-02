using Entity = HealthCoach.Shared.Core.Entity;

namespace HealthCoach.Core.Business;

public sealed class Exercise : Entity
{
    public Exercise() { }

    public Exercise(string name, string repRange, string restTime, int sets, string type)
    {
        Name = name;
        RepRange = repRange;
        RestTime = restTime;
        Sets = sets;
        Type = type;
    }

    public static Exercise Create(string name, string repRange, string restTime, int sets, string type)
        => new(name, repRange, restTime, sets, type);

    public string? Name { get; set; }

    public string? RepRange { get; set; }

    public string? RestTime { get; set; }

    public int? Sets { get; set; }

    public string? Type { get; set; }

    public FitnessPlan FitnessPlan { get; set; }
}