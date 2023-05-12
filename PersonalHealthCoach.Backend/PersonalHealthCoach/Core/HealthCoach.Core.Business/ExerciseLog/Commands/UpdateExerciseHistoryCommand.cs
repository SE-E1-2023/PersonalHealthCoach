using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record UpdateExerciseHistoryCommand(Guid UserId, IReadOnlyCollection<Exercise> Exercises) : IRequest<Result>;

public sealed record Exercise(string Title, int Calories, int Duration);