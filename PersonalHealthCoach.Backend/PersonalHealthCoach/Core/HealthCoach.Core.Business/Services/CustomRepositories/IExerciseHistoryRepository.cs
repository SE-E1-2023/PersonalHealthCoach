namespace HealthCoach.Core.Business;

public interface IExerciseHistoryRepository
{
    Task Store(Guid userId, IReadOnlyCollection<Exercise> exercises);
}