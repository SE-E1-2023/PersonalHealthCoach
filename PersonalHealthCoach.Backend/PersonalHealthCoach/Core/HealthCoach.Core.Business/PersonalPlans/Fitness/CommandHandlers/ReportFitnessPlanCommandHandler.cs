using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Report;

namespace HealthCoach.Core.Business;

internal class ReportFitnessPlanCommandHandler : IRequestHandler<ReportFitnessPlanCommand, Result>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public ReportFitnessPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result> Handle(ReportFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var tipResult = await repository.Load<FitnessPlan>(request.FitnessPlanId)
            .ToResult(Errors.FitnessPlanDoesNotExist);

        var reportResult = tipResult
               .Map(_ => queryProvider.Query<Report>()
               .FirstOrDefault(e => e.TargetId == request.FitnessPlanId))
               .Ensure(r => r == null, Errors.ReportAlreadyExists);

        return await reportResult
            .Bind(result => Report.Create(
                request.FitnessPlanId,
                nameof(FitnessPlan),
                request.Reason))
            .Tap(r => repository.Store(r));
    }
}
