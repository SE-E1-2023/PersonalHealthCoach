using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
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