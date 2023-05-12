using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.ExerciseHistory.AddExercises;

namespace HealthCoach.Core.Business;

internal sealed class UpdateExerciseHistoryCommandHandler : IRequestHandler<UpdateExerciseHistoryCommand, Result>
{
    private readonly IRepository repository;
    private readonly IExerciseHistoryRepository exerciseHistoryRepository;

    public UpdateExerciseHistoryCommandHandler(IRepository repository, IExerciseHistoryRepository exerciseHistoryRepository)
    {
        this.repository = repository;
        this.exerciseHistoryRepository = exerciseHistoryRepository;
    }

    public async Task<Result> Handle(UpdateExerciseHistoryCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository
            .Load<User>(request.UserId)
            .ToResult(Errors.UserNotFound);

        return await userResult.Tap(u => exerciseHistoryRepository.Store(u.Id, request.Exercises));
    }
}