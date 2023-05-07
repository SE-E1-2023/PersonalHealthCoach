using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record UpdateExerciseLogCommand(Guid UserId, IReadOnlyCollection<string> Exercises) : IRequest<Result>;