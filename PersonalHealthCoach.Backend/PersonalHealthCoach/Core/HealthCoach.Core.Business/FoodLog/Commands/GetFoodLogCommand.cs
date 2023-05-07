using CSharpFunctionalExtensions;
using MediatR;
using HealthCoach.Core.Domain;


namespace HealthCoach.Core.Business;

public record GetFoodLogCommand(Guid UserId) : IRequest<Result<FoodLog>>;


