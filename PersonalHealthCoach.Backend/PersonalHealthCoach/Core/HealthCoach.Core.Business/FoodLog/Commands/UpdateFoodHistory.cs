using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record UpdateFoodHistory(Guid UserId, IReadOnlyCollection<Food> Foods) : IRequest<Result>;

public sealed record Food(string Title, int Calories, int Quantity);