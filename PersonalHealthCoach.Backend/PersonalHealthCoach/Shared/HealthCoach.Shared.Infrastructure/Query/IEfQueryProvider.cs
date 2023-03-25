using HealthCoach.Shared.Core;

namespace HealthCoach.Shared.Infrastructure;

public interface IEfQueryProvider
{
    IQueryable<T> Query<T>() where T : AggregateRoot;
}