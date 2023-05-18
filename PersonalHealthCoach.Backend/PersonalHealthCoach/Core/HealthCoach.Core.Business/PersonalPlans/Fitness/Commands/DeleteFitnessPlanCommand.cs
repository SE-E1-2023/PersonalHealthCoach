using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record DeleteFitnessPlanCommand(Guid FitnessPlanId, Guid CallerId) : IRequest<Result>;