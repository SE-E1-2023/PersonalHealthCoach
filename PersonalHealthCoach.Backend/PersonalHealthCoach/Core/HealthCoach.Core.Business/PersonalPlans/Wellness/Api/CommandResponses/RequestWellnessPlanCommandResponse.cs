namespace HealthCoach.Core.Business;

internal sealed record RequestWellnessPlanCommandResponse(ApiActionResponse Action, IReadOnlyCollection<string> Categories);

internal sealed record ApiActionResponse(string Description, string Title);