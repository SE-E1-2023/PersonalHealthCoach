using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

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

    public IReadOnlyCollection<Workout> Workouts { get; private set; }

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