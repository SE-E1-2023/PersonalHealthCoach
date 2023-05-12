using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FoodHistory.Get;

namespace HealthCoach.Core.Business;

public class GetFoodHistoryCommandHandler : IRequestHandler<GetFoodHistoryCommand, Result<FoodHistory>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetFoodHistoryCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public async Task<Result<FoodHistory>> Handle(GetFoodHistoryCommand request, CancellationToken cancellationToken)
    {
        return queryProvider.
            Query<FoodHistory>().
            FirstOrDefault(f => f.Id == request.UserId).
            EnsureNotNull(Errors.FoodHistoryNotFound);
    }
}