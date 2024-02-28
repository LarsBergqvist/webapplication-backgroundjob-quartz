using Microsoft.Extensions.Options;
using Quartz;
using WebApplication.Abstractions;

namespace WebApplication.MessageBox;

internal sealed class ProcessMessagesBoxJobSetup : IConfigureOptions<QuartzOptions>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly MessageBoxOptions _messageBoxOptions;

    public ProcessMessagesBoxJobSetup(IOptions<MessageBoxOptions> messageboxOptions, IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _messageBoxOptions = messageboxOptions.Value;
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
                        schedule.WithIntervalInSeconds(_messageBoxOptions.IntervalInSeconds).RepeatForever())
                    .StartAt(_dateTimeProvider.UtcNow.AddSeconds(10)));
    }
}
