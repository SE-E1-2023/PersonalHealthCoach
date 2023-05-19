using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace HealthCoach.Core.Domain;

public sealed class FitnessPlan : AggregateRoot
{
    public FitnessPlan() { }

    private FitnessPlan(Guid userId, IReadOnlyCollection<Workout> workouts) : this()
    {
        UserId = userId;
        CreatedAt = TimeProvider.Instance().UtcNow;
        Workouts = workouts;
    }

    public static Result<FitnessPlan> Create(Guid userId, IReadOnlyCollection<Workout> workout)
    {
        return Result.Success()
            .Map(() => new FitnessPlan(userId, workout));
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<Workout> Workouts { get; set; }

    public DateTime CreatedAt { get; private set; }
}

public sealed class Workout : AggregateRoot
{
    public Workout()
    {
        
    }

    public Workout(IReadOnlyCollection<Exercise> exercises)
    {
        Exercises = exercises;
    }

    public IReadOnlyCollection<Exercise> Exercises { get; set; }
}

public sealed class Exercise : AggregateRoot
{
    public Exercise() { }

    private Exercise(string name, string repRange, string restTime, int sets, string type, List<string> images, List<string> instructions)
    {
        Name = name;
        RepRange = repRange;
        RestTime = restTime;
        Sets = sets;
        Type = type;
        Images = images;
        Instructions = instructions;
    }

    public static Exercise Create(string name, string repRange, string restTime, int sets, string type,
        List<string> images, List<string> instructions)
        => new(name, repRange, restTime, sets, type, images, instructions);

    [JsonPropertyName("exercise")]
    public string? Name { get; set; }

    [JsonPropertyName("rep_range")]
    public string? RepRange { get; set; }

    [JsonPropertyName("rest_time")]
    public string? RestTime { get; set; }

    [JsonPropertyName("sets")]
    public int? Sets { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("images")]
    public List<string> Images { get; set; }

    [JsonPropertyName("instructions")]
    public List<string> Instructions { get; set; }
}