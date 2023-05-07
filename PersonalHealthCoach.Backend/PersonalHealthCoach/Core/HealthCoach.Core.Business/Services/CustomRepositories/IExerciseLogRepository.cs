using HealthCoach.Core.Domain;

namespace HealthCoach.Core.Business;

public interface IExerciseLogRepository
{
    Task Store(Guid userId, IReadOnlyCollection<string> exercises);
}