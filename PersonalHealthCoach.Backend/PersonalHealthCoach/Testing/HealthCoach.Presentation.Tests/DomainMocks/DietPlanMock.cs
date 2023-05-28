using HealthCoach.Core.Domain;

namespace HealthCoach.Presentation.Tests;

public sealed record DietPlanMock(Guid UserId, string Name, string Scope, List<string> DietType, List<string> Recommendations, List<string> Interdictions, Meal Breakfast, Meal Drink, Meal MainCourse, Meal SideDish, Meal Snack, Meal Soup);