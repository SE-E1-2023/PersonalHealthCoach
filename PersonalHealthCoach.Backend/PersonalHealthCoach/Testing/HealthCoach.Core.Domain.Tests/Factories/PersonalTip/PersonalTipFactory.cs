namespace HealthCoach.Core.Domain.Tests;

public static class PersonalTipFactory
{
    public static PersonalTip Any() => PersonalTip.Create(Guid.NewGuid(), "general", "test tip").Value;

    public static PersonalTip Any(Guid userId) => PersonalTip.Create(userId, "general", "test tip").Value;
}