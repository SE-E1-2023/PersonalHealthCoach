using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;

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
        var query = dbContext.Set<T>().AsQueryable();

        var navigationProperties = typeof(T).GetProperties()
            .Where(p => (p.PropertyType.IsGenericType &&
                         (p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) ||
                          p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                          p.PropertyType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)) &&
                         typeof(AggregateRoot).IsAssignableFrom(p.PropertyType.GetGenericArguments()[0])) ||
                        (typeof(AggregateRoot).IsAssignableFrom(p.PropertyType) && !p.PropertyType.IsAbstract));

        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty.Name);
        }

        return query;
    }
}