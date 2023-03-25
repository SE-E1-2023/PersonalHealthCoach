using HealthCoach.Shared.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HealthCoach.Shared.Infrastructure;

public sealed class GenericDbContext : DbContext
{
    public GenericDbContext(DbContextOptions<GenericDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var aggregateRootTypes = GetAllAggregateRootTypes();

        foreach (var entityType in aggregateRootTypes)
        {
            modelBuilder.Entity(entityType);
        }
    }

    private IEnumerable<Type> GetAllAggregateRootTypes()
    {
        var aggregateRootType = typeof(AggregateRoot);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        var domainAssembly = "CarManagement.Domain";
        if (!assemblies.Any(a => a.GetName().Name == domainAssembly))
        {
            assemblies.Add(Assembly.Load(domainAssembly));
        }

        var aggregateRootTypes = new List<Type>();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(aggregateRootType));
            aggregateRootTypes.AddRange(types);
        }

        return aggregateRootTypes;
    }
}