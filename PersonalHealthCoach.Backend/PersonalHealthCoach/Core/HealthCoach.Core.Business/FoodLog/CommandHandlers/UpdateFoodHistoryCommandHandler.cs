using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FoodHistory.AddFoods;

namespace HealthCoach.Core.Business;

internal sealed class UpdateFoodHistoryCommandHandler : IRequestHandler<UpdateFoodHistoryCommand, Result>
{
    private readonly IRepository repository;
    private readonly IFoodHistoryRepository foodHistoryRepository;

    public UpdateFoodHistoryCommandHandler(IRepository repository, IFoodHistoryRepository foodHistoryRepository)
    {
        this.repository = repository;
        this.foodHistoryRepository = foodHistoryRepository;
    }

    public async Task<Result> Handle(UpdateFoodHistoryCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository
            .Load<User>(request.UserId)
            .ToResult(Errors.UserNotFound);

        return await userResult.Tap(u => foodHistoryRepository.Store(u.Id, request.Foods));
    }
}