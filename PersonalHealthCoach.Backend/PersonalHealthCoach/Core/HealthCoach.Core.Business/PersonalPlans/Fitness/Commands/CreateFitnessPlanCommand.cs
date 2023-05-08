using MediatR;
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;

namespace HealthCoach.Core.Business;

public sealed record CreateFitnessPlanCommand(Guid UserId) : IRequest<Result<FitnessPlan>>;