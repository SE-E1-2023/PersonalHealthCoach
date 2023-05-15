using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record CreateSingleUseDietPlanCommand(Guid UserId) : IRequest<Result<DietPlan>>;