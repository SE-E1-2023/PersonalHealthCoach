using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;

namespace HealthCoach.Shared.Infrastructure;

public sealed class GenericRepository : IRepository
{
    private readonly GenericDbContext dbContext;

    public GenericRepository(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Maybe<T>> Load<T>(Guid id) where T : AggregateRoot
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task Store<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
    {
        var entry = dbContext.Entry(aggregateRoot);

        if (entry.State == EntityState.Detached)
        {
            dbContext.Set<TAggregateRoot>().Add(aggregateRoot);
        }
        else
        {
            entry.State = EntityState.Modified;
        }

        await dbContext.SaveChangesAsync();
    }

}