namespace HealthCoach.Core.Domain.Tests;

public static class PersonalDataFactory
{
    public static PersonalData Any() => PersonalData.Create(Guid.NewGuid(),
            PersonalDataConstants.MinimumDateOfBirth,
            70,
            170,
            null,
            null,
            PersonalDataConstants.AllowedGoals.First(),
            null,
            12333,
            8.5,
            "M",
            true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false
        ).Value;

    public static PersonalData WithUserId(Guid userId) => PersonalData.Create(userId,
            PersonalDataConstants.MinimumDateOfBirth,
            70,
            170,
            null,
            null,
            PersonalDataConstants.AllowedGoals.First(),
            null,
            12333,
            8.5,
            "M",
            true, 5, true, true, true, true, true, true, true, true, false, true, true, true, false
        ).Value;
}