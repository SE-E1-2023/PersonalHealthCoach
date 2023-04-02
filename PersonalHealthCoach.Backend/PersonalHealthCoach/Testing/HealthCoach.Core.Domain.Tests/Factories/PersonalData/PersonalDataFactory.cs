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
        null).Value;

}