using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed class ExerciseHistory : AggregateRoot
{
    public ExerciseHistory() { }

    private ExerciseHistory(Guid userId) : this()
    {
        Id = userId;
        CompletedExercises = new List<CompletedExercise>();
        UpdatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<ExerciseHistory> Instance(Guid userId) => Result.Success(new ExerciseHistory(userId));

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

    private CompletedExercise(string title, int caloriesBurned, int duration) : this()
    {
        Title = title;
        CaloriesBurned = caloriesBurned;
        DurationInMinutes = duration;

        CompletedAt = TimeProvider.Instance().UtcNow;
        IsNew = false;
    }

    public static CompletedExercise Create(string title, int caloriesBurned, int duration) =>
        new(title, caloriesBurned, duration);

    public string Title { get; private set; }

    public DateTime CompletedAt { get; private set; }

    public int CaloriesBurned { get; private set; }

    public int DurationInMinutes { get; private set; }

    public bool IsNew { get; private set; }

    public void ResetIsNew()
    {
        IsNew = false;
    }
}