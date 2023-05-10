namespace HealthCoach.Shared.Web;

public static class ExternalEndpoints
{
    public static class AI
    {
        public const string BaseUrl = "http://localhost:8000";

        public const string TipGenerator = $"{nameof(TipGenerator)}";
        public const string DietPlanner = $"{nameof(DietPlanner)}";
        public const string FitnessPlanner = $"{nameof(FitnessPlanner)}";
        public const string Wellness = $"{nameof(Wellness)}";
    }
}