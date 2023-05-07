using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.ExerciseLog.AddExercises;

namespace HealthCoach.Core.Business;

internal sealed class UpdateExerciseLogCommandHandler : IRequestHandler<UpdateExerciseLogCommand, Result>
{
    private readonly IRepository repository;
    private readonly IExerciseLogRepository exerciseLogRepository;

    public UpdateExerciseLogCommandHandler(IRepository repository, IExerciseLogRepository exerciseLogRepository)
    {
        this.repository = repository;
        this.exerciseLogRepository = exerciseLogRepository;
    }

    public async Task<Result> Handle(UpdateExerciseLogCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository
            .Load<User>(request.UserId)
            .ToResult(Errors.UserNotFound);

        return await userResult.Tap(u => exerciseLogRepository.Store(u.Id, request.Exercises));
    }
}