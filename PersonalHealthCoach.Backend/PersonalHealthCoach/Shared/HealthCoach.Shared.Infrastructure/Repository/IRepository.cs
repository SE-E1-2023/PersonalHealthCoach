using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Shared.Infrastructure;

public interface IRepository
{
    Task<Maybe<TAggregateRoot>> Load<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;

    Task Store<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
}