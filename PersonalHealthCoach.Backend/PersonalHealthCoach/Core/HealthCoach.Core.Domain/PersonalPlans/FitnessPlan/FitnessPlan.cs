using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class FitnessPlan : AggregateRoot
{
    public FitnessPlan() { }

    public FitnessPlan(Guid userId, IReadOnlyCollection<Exercise> exercises) : this()
    {
        UserId = userId;
        Exercises = exercises;
        CreatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<FitnessPlan> Create(Guid userId, IReadOnlyCollection<Exercise> exercises)
    {
        var exercisesResult = exercises
            .EnsureNotNull(DomainErrors.FitnessPlan.Create.NoExercises)
            .Ensure(e => e.Any(), DomainErrors.FitnessPlan.Create.NoExercises);

        return exercisesResult
            .Map(e => new FitnessPlan(userId, e));
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<Exercise> Exercises { get; private set; }

    public DateTime CreatedAt { get; private set; }
}