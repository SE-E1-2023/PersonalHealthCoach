namespace HealthCoach.Core.Business;

internal sealed class RequestFitnessPlanCommandResponse
{
    public RequestFitnessPlanCommandResponse() { }

    public string message { get; set; }

    public string status { get; set; }

    public FitnessPlannerApiResponseWorkout workout { get; set; }
}

internal sealed record FitnessPlannerApiResponseWorkout(List<FitnessPlannerApiResponseExercise> workout);

internal sealed record FitnessPlannerApiResponseExercise(string exercise, string rep_range, string rest_time, int sets, string type);