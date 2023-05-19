using Newtonsoft.Json;

namespace HealthCoach.Core.Business;

internal sealed record RequestPersonalTipCommand(RequestProfile Profile, List<RequestProgress> Progress);

internal sealed record RequestProfile(
    string Name,
    string ID,
    int Age,
    int Height,
    int Weight,
    string Sex,
    string Objective,
    [JsonProperty("Level of activity")] string LevelOfActivity,
    [JsonProperty("Steps Goal")] int StepsGoal
);

internal sealed record RequestProgress(
    string Day,
    string Date,
    int Weight,
    string Objective,
    List<RequestFoodHistory> FoodLogs,
    List<RequestExerciseHistory> ExerciseLogs,
    int Steps,
    int HoursSlept
);

internal sealed record RequestFoodHistory(string Meal, string FoodItem, int Quantity, int Calories);

internal sealed record RequestExerciseHistory(string Exercise, int Duration, int CaloriesBurned);