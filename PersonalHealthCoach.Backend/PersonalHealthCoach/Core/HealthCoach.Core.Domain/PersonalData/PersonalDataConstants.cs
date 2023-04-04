using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain;

public static class PersonalDataConstants
{
    public static int MinimumAge => 18;

    public static DateTime MinimumDateOfBirth => TimeProvider.Instance().UtcNow.AddYears(-MinimumAge);

    public static IReadOnlyCollection<string> AllowedGoals => new List<string>
    {
        "Lose weight",
        "Gain muscular mass",
        "Improve overall health",
        "Improve cardiovascular health",
        "Increase strength",
        "Increase endurance",
        "Maintain weigth"
    };
}
