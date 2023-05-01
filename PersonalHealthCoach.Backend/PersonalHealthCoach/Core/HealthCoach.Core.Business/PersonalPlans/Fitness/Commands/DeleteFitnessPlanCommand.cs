using CSharpFunctionalExtensions;
using MediatR;
using HealthCoach.Core.Domain;

namespace HealthCoach.Core.Business;

public sealed record DeleteFitnessPlanCommand(Guid PlanId) : IRequest<Result<FitnessPlan>>;