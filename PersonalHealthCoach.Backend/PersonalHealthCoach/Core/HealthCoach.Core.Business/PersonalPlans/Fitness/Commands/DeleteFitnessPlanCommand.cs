using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record DeleteFitnessPlanCommand(Guid FitnessPlanId) : IRequest<Result>;