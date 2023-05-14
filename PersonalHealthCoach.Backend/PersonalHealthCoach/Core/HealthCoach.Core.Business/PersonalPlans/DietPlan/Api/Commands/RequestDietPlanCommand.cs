namespace HealthCoach.Core.Business;

internal sealed record RequestDietPlanCommand(string idClient, List<string> alergies, string dietType, string goal, string requestType);