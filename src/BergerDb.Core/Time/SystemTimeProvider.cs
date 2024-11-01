namespace BergerDb.Core.Time;

public static class SystemTimeProvider
{
    public static Func<DateTime> UtcNow { get; internal set; } = () => DateTime.UtcNow;
}
