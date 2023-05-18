using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Get;

namespace HealthCoach.Core.Business;

internal class GetLatestFitnessPlanCommandHandler : IRequestHandler<GetLatestFitnessPlanCommand, Result<FitnessPlan>>
{
    private readonly IEfQueryProvider queryProvider;
    private readonly IFitnessPlanRepository repository;

    public GetLatestFitnessPlanCommandHandler(IEfQueryProvider queryProvider, IFitnessPlanRepository repository)
    {
        this.queryProvider = queryProvider;
        this.repository = repository;
    }

    public async Task<Result<FitnessPlan>> Handle(GetLatestFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var planResult = queryProvider
         .Query<FitnessPlan>()
         .Where(pd => pd.UserId == request.UserId)
         .OrderByDescending(pd => pd.CreatedAt)
         .FirstOrDefault()
         .EnsureNotNull(Errors.FitnessPlanNotFound)!;

        if (planResult.IsFailure)
        {
            return planResult;
        }

        return await Result.Success().Map(() => repository.Load(request.UserId));
    }
}
