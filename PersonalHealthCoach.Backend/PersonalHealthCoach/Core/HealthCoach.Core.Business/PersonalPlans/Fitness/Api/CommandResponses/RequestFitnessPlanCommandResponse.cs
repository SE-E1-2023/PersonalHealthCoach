using HealthCoach.Core.Domain;

namespace HealthCoach.Core.Business;

internal sealed class RequestFitnessPlanCommandResponse
{
    public RequestFitnessPlanCommandResponse() { }

    public int status { get; set; }

    public FitnessPlannerApiResponseWorkout workouts { get; set; }
}

internal sealed record FitnessPlannerApiResponseWorkout(List<Domain.Exercise> workout1, List<Domain.Exercise> workout2, List<Domain.Exercise> workout3, List<Domain.Exercise> workout4, List<Domain.Exercise> workout5, List<Domain.Exercise> workout6, List<Domain.Exercise> workout7);

internal sealed record FitnessPlannerApiResponseExercise(string exercise, string rep_range, string rest_time, int sets, string type);