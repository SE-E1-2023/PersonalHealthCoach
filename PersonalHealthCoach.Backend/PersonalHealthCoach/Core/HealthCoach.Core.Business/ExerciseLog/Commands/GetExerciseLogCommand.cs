using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public record GetExerciseLogCommand(Guid UserId) : IRequest<Result<ExerciseLog>>;