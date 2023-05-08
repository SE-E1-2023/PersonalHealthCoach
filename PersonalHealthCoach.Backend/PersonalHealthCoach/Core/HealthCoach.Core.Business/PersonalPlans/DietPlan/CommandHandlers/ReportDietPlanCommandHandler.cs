
using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;

namespace HealthCoach.Core.Business;
using Errors = HealthCoach.Core.Business.BusinessErrors.DietPlan.Report;

internal class ReportDietPlanCommandHandler : IRequestHandler<ReportDietPlanCommand, Result>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public ReportDietPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result> Handle(ReportDietPlanCommand request, CancellationToken cancellationToken)
    {
        var tipResult = await repository.Load<DietPlan>(request.DietPlanId)
            .ToResult(Errors.DietPlanDoesNotExist);

        var reportResult = tipResult
            .Ensure(t => !queryProvider.Query<Report>().Any(r => r.TargetId == t.Id), Errors.ReportAlreadyExists);

        return await reportResult
            .Bind(result => Report.Create(request.DietPlanId, nameof(DietPlan), request.Reason))
            .Tap(r => repository.Store(r));
    }
}
