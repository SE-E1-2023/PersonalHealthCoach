using HealthCoach.Shared.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<GenericDbContext>(options =>
            options.UseNpgsql(connectionString));

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GenericDbContext>();
        dbContext.Database.Migrate();
    })
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

    public static GenericDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<GenericDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseNpgsql(connectionString);

        return new GenericDbContext(builder.Options);
    }

    private static IServiceCollection AddHealthCoachAppBusiness(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddHealthCoachAppInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}