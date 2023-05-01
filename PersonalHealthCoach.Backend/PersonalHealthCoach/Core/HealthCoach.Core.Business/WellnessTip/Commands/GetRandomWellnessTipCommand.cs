using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public sealed record GetRandomWellnessTipCommand : IRequest<Result<WellnessTip>>;
