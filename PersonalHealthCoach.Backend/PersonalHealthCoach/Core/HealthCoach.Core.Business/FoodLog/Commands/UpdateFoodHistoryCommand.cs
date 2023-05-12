using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record UpdateFoodHistoryCommand(Guid UserId, IReadOnlyCollection<Food> Foods) : IRequest<Result>;

public sealed record Food(string Title, string Meal, int Calories, int Quantity);