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
            "M"
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
            "M"
        ).Value;
}