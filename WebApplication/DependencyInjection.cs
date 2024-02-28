using Quartz;
using WebApplication.Abstractions;
using WebApplication.Outbox;

namespace WebApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        AddBackgroundJobs(services, configuration);

        return services;
    }
    
    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBoxOptions>(configuration.GetSection("MessageBox"));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessMessagesBoxJobSetup>();
    }
}
