
using Newtonsoft.Json;

namespace HealthCoach.Core.Business;

internal sealed record RequestFitnessPlanCommand(string user_id, bool pro_user, string goal, int workouts_per_week, int fitness_score, RequestExercises equipment_available);

internal sealed class RequestExercises
{
    public RequestExercises(bool other, bool machine, bool barbell, bool dumbbell, bool kettlebells, bool cable, bool easyCurlBar, bool none, bool bands, bool medicineBall, bool exerciseBall, bool foamRoll, bool bodyOnly)
    {
        Other = other;
        Machine = machine;
        Barbell = barbell;
        Dumbbell = dumbbell;
        Kettlebells = kettlebells;
        Cable = cable;
        EasyCurlBar = easyCurlBar;
        None = none;
        Bands = bands;
        MedicineBall = medicineBall;
        ExerciseBall = exerciseBall;
        FoamRoll = foamRoll;
        BodyOnly = bodyOnly;
    }

    public bool Other { get; set; }

    public bool Machine { get; set; }

    public bool Barbell { get; set; }

    public bool Dumbbell { get; set; }

    public bool Kettlebells { get; set; }

    public bool Cable { get; set; }

    [JsonProperty("E-Z Curl Bar")]
    public bool EasyCurlBar { get; set; }

    public bool None { get; set; }

    public bool Bands { get; set; }

    [JsonProperty("Medicine Ball")]
    public bool MedicineBall { get; set; }

    [JsonProperty("Exercise Ball")]
    public bool ExerciseBall { get; set; }

    [JsonProperty("Foam Roll")]
    public bool FoamRoll { get; set; }

    [JsonProperty("Body Only")]
    public bool BodyOnly { get; set; }

}
