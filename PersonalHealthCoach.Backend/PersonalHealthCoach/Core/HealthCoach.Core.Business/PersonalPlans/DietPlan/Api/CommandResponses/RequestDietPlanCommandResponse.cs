using System.Reflection.Metadata.Ecma335;

namespace HealthCoach.Core.Business;

internal sealed class RequestDietPlanCommandResponse
{
    public RequestDietPlanCommandResponse() { }

    public int NOP { get; set; }

    public DietPlannerApiResponseMeal breakfast { get; set; }

    public DietPlannerApiResponseMeal drink { get; set; }

    public DietPlannerApiResponseMeal mainCourse { get; set; }

    public DietPlannerApiResponseMeal sideDish { get; set; }

    public DietPlannerApiResponseMeal snack { get; set; }

    public DietPlannerApiResponseMeal soup { get; set; }

    public DietPlannerApiResponseDiet diet { get; set; }
}

internal sealed record DietPlannerApiResponseMeal(int id, string image, List<string> ingredients, double kcal, string title);
internal sealed record DietPlannerApiResponseDiet(List<string> todo, List<string> donot, int id, string name, List<string> tag, string use);