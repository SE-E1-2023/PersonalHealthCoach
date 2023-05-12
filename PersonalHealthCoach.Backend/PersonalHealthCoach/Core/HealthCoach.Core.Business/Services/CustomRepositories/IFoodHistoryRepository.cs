namespace HealthCoach.Core.Business;

public interface IFoodHistoryRepository
{
    Task Store(Guid userId, IReadOnlyCollection<Food> Foods);
}