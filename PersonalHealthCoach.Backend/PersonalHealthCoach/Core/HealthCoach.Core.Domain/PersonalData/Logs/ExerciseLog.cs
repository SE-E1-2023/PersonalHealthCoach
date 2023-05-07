using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class ExerciseLog : AggregateRoot
{
    public ExerciseLog() { }

    private ExerciseLog(Guid userId) : this()
    {
        Id = userId;
        CompletedExercises = new List<CompletedExercise>();
        UpdatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<ExerciseLog> Instance(Guid userId) => Result.Success(new ExerciseLog(userId));

    public List<CompletedExercise> CompletedExercises { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void AddExercises(IReadOnlyCollection<CompletedExercise> exercises)
    {
        UpdatedAt = TimeProvider.Instance().UtcNow;

        CompletedExercises.AddRange(exercises);
    }
}

public sealed class CompletedExercise : AggregateRoot
{
    public CompletedExercise() { }

    private CompletedExercise(string title) : this()
    {
        Title = title;
        CompletedAt = TimeProvider.Instance().UtcNow;
        IsNew = false;
    }

    public static CompletedExercise Create(string title) => new(title);

    public string Title { get; private set; }

    public DateTime CompletedAt { get; private set; }

    public bool IsNew { get; private set; }

    public void ResetIsNew()
    {
        IsNew = false;
    }
}