using HealthCoach.Core.Business;
using HealthCoach.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("local.settings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureHealthCoachAppServices()
    .Build();

host.Run();

static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureHealthCoachAppServices(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureServices((_, services) => services
                .AddLogging(b => b.AddSimpleConsole())
                .AddHealthCoachAppBusiness()
                .AddHealthCoachAppInfrastructure()
            );
    }
}