using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record DeleteDietPlanCommand(Guid DietPlanId) : IRequest<Result>;
