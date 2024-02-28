using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using WebApplication.Abstractions;

namespace WebApplication.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessMessagesBoxJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly MessageBoxOptions _messageBoxOptions;
    private readonly ILogger<ProcessMessagesBoxJob> _logger;

    public ProcessMessagesBoxJob(
        IDateTimeProvider dateTimeProvider,
        IOptions<MessageBoxOptions> outboxOptions,
        ILogger<ProcessMessagesBoxJob> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
        _messageBoxOptions = outboxOptions.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Beginning to process messages");

        _logger.LogInformation("Completed processing messages");

        await Task.CompletedTask;
    }

}
