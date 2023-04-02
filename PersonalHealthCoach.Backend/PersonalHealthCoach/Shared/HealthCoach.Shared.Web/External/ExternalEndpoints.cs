namespace HealthCoach.Shared.Web;

public static class ExternalEndpoints
{
    public static class AI
    {
        private const string BaseUrl = "http://localhost:8000";

        public const string TipGenerator = $"{BaseUrl}/{nameof(TipGenerator)}";
        public const string DietPlanner = $"{BaseUrl}/{nameof(DietPlanner)}";
        public const string ExercisePlanner = $"{BaseUrl}/{nameof(ExercisePlanner)}";
    }
}