using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public record GetLatestFitnessPlanCommand(Guid UserId) : IRequest<Result<FitnessPlan>>;
