using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Business;

public sealed class FitnessPlan : AggregateRoot
{
    public FitnessPlan() { }

    public FitnessPlan(IReadOnlyCollection<Exercise> exercises) : this()
    {
        Exercises = exercises;
        CreatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<FitnessPlan> Create(IReadOnlyCollection<Exercise> exercises)
    {
        var exercisesResult = Result.SuccessIf(exercises.Any(), DomainErrors.FitnessPlan.Create.NoExercises);

        return exercisesResult
            .Map(() => new FitnessPlan(exercises));
    }

    public IReadOnlyCollection<Exercise> Exercises { get; private set; }

    public DateTime CreatedAt { get; private set; }
}