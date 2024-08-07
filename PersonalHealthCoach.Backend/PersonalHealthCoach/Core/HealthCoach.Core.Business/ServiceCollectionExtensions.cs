﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCoach.Core.Business;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthCoachAppBusiness(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(BusinessAssembly))
            .AddScoped<IExerciseHistoryRepository, ExerciseHistoryRepository>()
            .AddScoped<IFoodHistoryRepository, FoodHistoryRepository>()
            .AddScoped<IFitnessPlanRepository, FitnessPlanRepository>();
    }

    private static class BusinessAssembly { }
}