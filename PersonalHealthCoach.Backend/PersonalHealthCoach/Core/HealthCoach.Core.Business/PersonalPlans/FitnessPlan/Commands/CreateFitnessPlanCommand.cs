using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record CreateFitnessPlanCommand(Guid UserId) : IRequest<Result<FitnessPlan>>;