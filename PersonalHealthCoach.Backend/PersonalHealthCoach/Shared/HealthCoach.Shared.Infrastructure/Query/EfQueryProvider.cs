using HealthCoach.Shared.Core;

namespace HealthCoach.Shared.Infrastructure;

public sealed class EfQueryProvider : IEfQueryProvider
{
    private readonly GenericDbContext dbContext;

    public EfQueryProvider(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IQueryable<T> Query<T>() where T : AggregateRoot
    {
        return dbContext.Set<T>().AsQueryable();
    }
}