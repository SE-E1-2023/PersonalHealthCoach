
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Get;

namespace HealthCoach.Core.Business;

internal class GetLatestFitnessPlanCommandHandler : IRequestHandler<GetLatestFitnessPlanCommand, Result<FitnessPlan>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetLatestFitnessPlanCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public async Task<Result<FitnessPlan>> Handle(GetLatestFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        return queryProvider
         .Query<FitnessPlan>()
         .Where(pd => pd.UserId == request.UserId)
         .OrderByDescending(pd => pd.CreatedAt)
         .FirstOrDefault()
         .EnsureNotNull(Errors.FitnessPlanNotFound)!;
    }
}
