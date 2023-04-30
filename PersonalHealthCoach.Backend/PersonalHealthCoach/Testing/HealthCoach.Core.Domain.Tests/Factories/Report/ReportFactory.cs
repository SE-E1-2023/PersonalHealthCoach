namespace HealthCoach.Core.Domain.Tests;

public static class ReportFactory
{
    public static Report Any() => Report.Create(
        Guid.NewGuid(),
        nameof(PersonalTip),
        "reason text").Value;
}