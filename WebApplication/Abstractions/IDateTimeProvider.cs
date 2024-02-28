namespace WebApplication.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
