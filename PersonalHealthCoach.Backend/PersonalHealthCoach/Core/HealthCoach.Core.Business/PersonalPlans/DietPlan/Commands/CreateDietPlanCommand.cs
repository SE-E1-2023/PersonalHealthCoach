using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record CreateDietPlanCommand(Guid UserId) : IRequest<Result<DietPlan>>;