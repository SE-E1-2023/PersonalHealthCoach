using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record CreateSingleUseFitnessPlanCommand(Guid UserId, int FitnessScore) : IRequest<Result<FitnessPlan>>;