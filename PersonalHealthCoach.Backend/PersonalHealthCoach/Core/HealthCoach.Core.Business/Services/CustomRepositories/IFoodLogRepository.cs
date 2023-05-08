using HealthCoach.Core.Domain;

namespace HealthCoach.Core.Business;

public interface IFoodLogRepository
{
    Task Store(Guid userId, IReadOnlyCollection<string> foods);
}