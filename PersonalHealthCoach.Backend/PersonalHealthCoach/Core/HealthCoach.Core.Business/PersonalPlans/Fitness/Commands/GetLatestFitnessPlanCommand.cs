
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public record GetLatestFitnessPlanCommand(Guid UserId) : IRequest<Result<FitnessPlan>>;
