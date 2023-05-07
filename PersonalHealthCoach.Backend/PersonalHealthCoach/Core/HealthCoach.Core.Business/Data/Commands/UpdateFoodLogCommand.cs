using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record UpdateFoodLogCommand(Guid UserId, IReadOnlyCollection<string> Foods ) : IRequest<Result>;