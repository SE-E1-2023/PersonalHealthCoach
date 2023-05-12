using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public record GetExerciseHistoryCommand(Guid UserId) : IRequest<Result<ExerciseHistory>>;