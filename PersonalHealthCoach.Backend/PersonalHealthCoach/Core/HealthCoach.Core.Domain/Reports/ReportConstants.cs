namespace HealthCoach.Core.Domain;

public static class ReportConstants
{
    public static IReadOnlyCollection<string> AllowedTargets => new List<string>
    {
        nameof(FitnessPlan),
        nameof(PersonalTip),
        nameof(DietPlan)
    };
}