using Microsoft.Extensions.Options;
using Quartz;

namespace WebApplication.Outbox;

internal sealed class ProcessMessagesBoxJobSetup : IConfigureOptions<QuartzOptions>
{
    private readonly MessageBoxOptions _messageBoxOptions;

    public ProcessMessagesBoxJobSetup(IOptions<MessageBoxOptions> outboxOptions)
    {
        _messageBoxOptions = outboxOptions.Value;
    }

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessMessagesBoxJob);

        options
            .AddJob<ProcessMessagesBoxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_messageBoxOptions.IntervalInSeconds).RepeatForever()));
    }
}
