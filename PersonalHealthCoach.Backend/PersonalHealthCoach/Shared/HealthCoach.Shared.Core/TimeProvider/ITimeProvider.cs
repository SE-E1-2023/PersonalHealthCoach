namespace HealthCoach.Shared.Core;

public interface ITimeProvider
{
    DateTime UtcNow { get; }
}