namespace HealthCoach.Shared.Core;

public abstract class AggregateRoot : Entity<Guid>, IAggregateRoot
{
    protected AggregateRoot()
    {
        Id = Guid.NewGuid();
    }
}