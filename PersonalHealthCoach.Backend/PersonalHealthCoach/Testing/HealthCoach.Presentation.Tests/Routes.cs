namespace HealthCoach.Presentation.Tests;

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
        public const string GetFoodHistory = $"{Prefix}/users/{{id}}/food-history";
    }
}