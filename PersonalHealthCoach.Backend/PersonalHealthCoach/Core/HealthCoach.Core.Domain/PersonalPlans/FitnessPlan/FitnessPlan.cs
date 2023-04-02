using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Business;

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
        var exercisesResult = Result.SuccessIf(exercises.Any(), DomainErrors.FitnessPlan.Create.NoExercises);

        return exercisesResult
            .Map(() => new FitnessPlan(userId, exercises));
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<Exercise> Exercises { get; private set; }

    public DateTime CreatedAt { get; private set; }
}