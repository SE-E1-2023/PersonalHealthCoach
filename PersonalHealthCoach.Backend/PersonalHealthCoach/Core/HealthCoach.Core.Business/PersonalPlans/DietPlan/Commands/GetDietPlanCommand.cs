
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record GetDietPlanCommand(Guid UserId) : IRequest<Result<DietPlan>>;
