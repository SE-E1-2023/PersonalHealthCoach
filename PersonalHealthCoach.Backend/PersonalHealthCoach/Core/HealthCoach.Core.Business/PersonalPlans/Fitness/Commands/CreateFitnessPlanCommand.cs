using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record CreateFitnessPlanCommand(Guid UserId) : IRequest<Result<FitnessPlan>>;