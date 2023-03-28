using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCoach.Core.Business;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthCoachAppBusiness(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(assembly);
    }

    private static class BusinessAssembly { }
}