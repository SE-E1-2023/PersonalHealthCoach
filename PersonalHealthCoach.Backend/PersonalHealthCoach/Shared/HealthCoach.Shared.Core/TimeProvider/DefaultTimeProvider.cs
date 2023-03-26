namespace HealthCoach.Shared.Core;

internal class DefaultTimeProvider : ITimeProvider
{
    public DateTime UtcNow => TimeProviderContext.Current?.Time ?? DateTime.UtcNow;
}