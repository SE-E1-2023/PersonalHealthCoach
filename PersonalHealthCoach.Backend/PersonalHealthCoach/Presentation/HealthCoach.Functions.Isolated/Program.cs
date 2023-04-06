using HealthCoach.Core.Business;
using HealthCoach.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

using (var scope = host.Services.CreateScope())
{
    var databaseSeeder = scope.ServiceProvider.GetRequiredService<DbPopulationService>();
    await databaseSeeder.PopulateDb();
}

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
                .AddSingleton<DbPopulationService>()
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

        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Npgsql.NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}