using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;

namespace HealthCoach.Core.Business;

internal sealed class GetDietPlanCommandHandler : IRequestHandler<GetDietPlanCommand, Result<DietPlan>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetDietPlanCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public async Task<Result<DietPlan>> Handle(GetDietPlanCommand request, CancellationToken cancellationToken)
    {
        return Result.Success(queryProvider.Query<DietPlan>().OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.UserId == request.UserId));
    }
}
