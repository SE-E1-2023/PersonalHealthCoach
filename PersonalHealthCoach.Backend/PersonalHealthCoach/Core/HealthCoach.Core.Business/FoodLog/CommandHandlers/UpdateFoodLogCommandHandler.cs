using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FoodLog.Create;
using HealthCoach.Core.Business.FoodLog.Commands;

namespace HealthCoach.Core.Business.FoodLog.CommandHandlers;

internal class UpdateFoodLogCommandHandler : IRequestHandler<UpdateFoodLogCommand, Result>
{
    private readonly IRepository repository;

    public UpdateFoodLogCommandHandler(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> Handle(UpdateFoodLogCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository
            .Load<User>(request.UserId)
            .ToResult(Errors.UserNotFound);

        return await userResult
            .Map(u => repository.Load<FoodLog>(u.Id))
            .Map(l => l.HasValue ? FoodLog.Instance(userResult.Value.Id).Value : l.Value)
            .Tap(l => l.AddFoods(request.Foods))
            .Tap(l => repository.Store(l));
    }
}

