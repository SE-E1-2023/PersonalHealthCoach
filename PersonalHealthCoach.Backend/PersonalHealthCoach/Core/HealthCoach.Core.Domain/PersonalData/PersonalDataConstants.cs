
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain;

public static class PersonalDataConstants
{
    public static int MinimumAge { get; } = 18;

    public static DateTime MinimumDateOfBirth { get => TimeProvider.Instance().UtcNow.AddYears(-MinimumAge); }

    public static IReadOnlyCollection<string> AllowedGoals { get; } = new List<string>
    {
        "Slabire",
        "Crestere",
        "Crestere masa musculara",
        "Mentinere"
    };
}
