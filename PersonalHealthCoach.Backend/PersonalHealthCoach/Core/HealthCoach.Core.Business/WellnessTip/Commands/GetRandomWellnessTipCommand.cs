using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Domain;
public sealed record GetRandomWellnessTipCommand() : IRequest<Result<WellnessTip>>;
