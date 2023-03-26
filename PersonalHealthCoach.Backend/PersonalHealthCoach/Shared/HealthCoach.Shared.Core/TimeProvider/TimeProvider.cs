namespace HealthCoach.Shared.Core;

public static class TimeProvider
{
    private static readonly Lazy<ITimeProvider> instance = new Lazy<ITimeProvider>(() => new DefaultTimeProvider());

    public static ITimeProvider Instance()
    {
        return instance.Value;
    }
}