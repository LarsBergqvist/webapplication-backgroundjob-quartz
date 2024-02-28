namespace WebApplication.Outbox;

public sealed class MessageBoxOptions
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
