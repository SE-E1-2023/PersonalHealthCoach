namespace HealthCoach.Core.Business;

internal sealed class RequestFitnessPlanCommandResponse
{
    public RequestFitnessPlanCommandResponse() { }

    public IReadOnlyCollection<FitnessPlannerApiResponseExercise> workout { get; set; }
}

internal sealed record FitnessPlannerApiResponseExercise(string exercise, string rep_range, string rest_time, int sets, string type);