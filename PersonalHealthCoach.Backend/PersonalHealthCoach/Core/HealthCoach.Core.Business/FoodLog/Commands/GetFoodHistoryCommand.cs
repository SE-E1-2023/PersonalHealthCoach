using CSharpFunctionalExtensions;
using MediatR;
using HealthCoach.Core.Domain;


namespace HealthCoach.Core.Business;

public record GetFoodHistoryCommand(Guid UserId) : IRequest<Result<FoodHistory>>;


