﻿namespace HealthCoach.Presentation.Tests;

internal static class Routes
{
    private const string Prefix = "http://localhost:7071/api/v1";

    public static class User
    {
        public const string CreateUser = $"{Prefix}/users";
        public const string GetUserByEmailAddress = $"{Prefix}/users?EmailAddress={{0}}";
    }

    public static class PersonalData
    {
        public const string AddPersonalData = $"{Prefix}/users/{{0}}/data/personal";
        public const string GetAllPersonalData = $"{Prefix}/users/{{0}}/data/personal";
        public const string RetrieveLatestPersonalData = $"{Prefix}/users/{{0}}/data/personal/latest";
    }

    public static class WellnessPlan
    {
        public const string CreateWellnessPlan = $"{Prefix}/users/{{0}}/plans/wellness";
    }

    public static class FoodHistory
    {
        public const string UpdateFoodHistory = $"{Prefix}/users/{{0}}/food-history";
        public const string GetFoodHistory = $"{Prefix}/users/{{0}}/food-history";
    }

    public static class ExerciseHistory
    {
        public const string UpdateExerciseHistory = $"{Prefix}/users/{{0}}/exercise-history";
        public const string GetExerciseHistory = $"{Prefix}/users/{{0}}/exercise-history";
    }

    public static class DietPlan
    {
        public const string CreateDietPlan = $"{Prefix}/users/{{0}}/plans/diet";
        public const string GetDietPlan = $"{Prefix}/users/{{0}}/plans/diet";
    }
}