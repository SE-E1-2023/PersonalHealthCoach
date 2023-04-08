using HealthCoach.Shared.Web;
using HealthCoach.Shared.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCoach.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthCoachAppInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<IEfQueryProvider, EfQueryProvider>()
            .AddScoped<IRepository, GenericRepository>()
            .AddScoped<IHttpClientFactory, HttpClientFactory>();
    }
}