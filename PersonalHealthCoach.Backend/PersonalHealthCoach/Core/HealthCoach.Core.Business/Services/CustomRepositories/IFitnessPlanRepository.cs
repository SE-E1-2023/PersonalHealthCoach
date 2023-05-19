using HealthCoach.Core.Domain;

namespace HealthCoach.Core.Business;

public interface IFitnessPlanRepository
{
    public Task<FitnessPlan> Load(Guid userId);
}
