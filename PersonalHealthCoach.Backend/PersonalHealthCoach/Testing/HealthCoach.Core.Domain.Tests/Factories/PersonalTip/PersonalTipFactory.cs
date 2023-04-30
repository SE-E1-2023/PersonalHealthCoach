namespace HealthCoach.Core.Domain.Tests;

public static class PersonalTipFactory
{
    public static PersonalTip Any() => PersonalTip.Create(
        Guid.NewGuid(),
        "general",
        "test tip").Value;
}