namespace HealthCoach.Core.Business;

public sealed record RequestWellnessPlanCommand(IReadOnlyCollection<string> Diseases);