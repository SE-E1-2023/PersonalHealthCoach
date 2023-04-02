using HealthCoach.Core.Business;
using HealthCoach.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HealthCoach.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

await HostBuilderExtensions.CreateAndApplyMigrationAsync();

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
                .AddDbContext()
            );
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<GenericDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }

    public static async Task CreateAndApplyMigrationAsync()
    {
        var factory = new GenericDbContextFactory();
        await using var dbContext = factory.CreateDbContext(args: null);

        await dbContext.Database.MigrateAsync();
    }
}