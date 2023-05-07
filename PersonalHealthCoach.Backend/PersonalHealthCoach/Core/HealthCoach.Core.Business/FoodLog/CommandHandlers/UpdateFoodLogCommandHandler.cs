using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FoodLog.AddFoods;

namespace HealthCoach.Core.Business;

internal sealed class UpdateFoodLogCommandHandler : IRequestHandler<UpdateFoodLogCommand, Result>
{
    private readonly IRepository repository;
    private readonly IFoodLogRepository foodLogRepository;

    public UpdateFoodLogCommandHandler(IRepository repository, IFoodLogRepository foodLogRepository)
    {
        this.repository = repository;
        this.foodLogRepository = foodLogRepository;
    }

    public async Task<Result> Handle(UpdateFoodLogCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository
            .Load<User>(request.UserId)
            .ToResult(Errors.UserNotFound);

        return await userResult.Tap(u => foodLogRepository.Store(u.Id, request.Foods));
    }
}