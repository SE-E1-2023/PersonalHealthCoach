namespace HealthCoach.Shared.Core;

public abstract class Entity<T> : IEquatable<Entity<T>>
{
    public T Id { get; protected set; }

    public bool Equals(Entity<T> other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Entity<T>)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public abstract class Entity : Entity<Guid>
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}