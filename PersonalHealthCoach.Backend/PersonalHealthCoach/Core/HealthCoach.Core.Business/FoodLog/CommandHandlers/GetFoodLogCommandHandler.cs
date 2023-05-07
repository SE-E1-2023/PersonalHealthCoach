using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FoodLog.Get;

namespace HealthCoach.Core.Business;

public class GetFoodLogCommandHandler : IRequestHandler<GetFoodLogCommand, Result<FoodLog>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetFoodLogCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public async Task<Result<FoodLog>> Handle(GetFoodLogCommand request, CancellationToken cancellationToken)
    {
        return queryProvider.
            Query<FoodLog>().
            FirstOrDefault(f => f.Id == request.UserId).
            EnsureNotNull(Errors.FoodLogNotFound);
    }
}