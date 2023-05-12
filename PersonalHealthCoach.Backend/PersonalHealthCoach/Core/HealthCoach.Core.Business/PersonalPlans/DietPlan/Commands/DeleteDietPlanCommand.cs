using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record DeleteDietPlanCommand(Guid DietPlanId, Guid CallerId) : IRequest<Result>;
