namespace HealthCoach.Shared.Web;

public static class ExternalEndpoints
{
    public static class AI
    {
        public const string BaseUrl = "http://localhost:8000";

        public const string TipGenerator = $"{nameof(TipGenerator)}";
        public const string DietPlanner = $"{nameof(DietPlanner)}";
        public const string FitnessPlanner = $"{nameof(FitnessPlanner)}";
    }
}

public static class InternalEndpoints
{
    public const string BaseUrl = "http://localhost:7071/api";

    public const string DeleteFitnessPlan = "v1/plans/fitness/";
    public const string DeleteDietPlan = "v1/plans/diet/";
    public const string DeletePersonalTip = "v1/plans/tips/";
}