namespace HealthCoach.Shared.Core;

public class TimeProviderContext : IDisposable
{
    private static readonly ThreadLocal<Stack<TimeProviderContext>> _threadScopeStack = new(() => new Stack<TimeProviderContext>());

    private TimeProviderContext(DateTime currentTime)
    {
        Time = currentTime;
    }

    public static TimeProviderContext Current => _threadScopeStack.Value.Count == 0 ? null : _threadScopeStack.Value.Peek();

    public static DateTime AdvanceTimeTo(DateTime time)
    {
        _threadScopeStack.Value.Push(new TimeProviderContext(time));
        return time;
    }

    public static DateTime AdvanceTimeToNow() => AdvanceTimeTo(DateTime.Now);

    public DateTime Time { get; }

    public void Dispose()
    {
        _threadScopeStack.Value.Pop();
    }
}