using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalTip.Report;

namespace HealthCoach.Core.Business;

internal class ReportPersonalTipCommandHandler : IRequestHandler<ReportPersonalTipCommand, Result>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public ReportPersonalTipCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result> Handle(ReportPersonalTipCommand request, CancellationToken cancellationToken)
    {
        var tipResult = await repository
            .Load<PersonalTip>(request.PersonalTipId)
            .ToResult(Errors.PersonalTipDoesNotExist);

        var reportResult = tipResult
            .Ensure(t => !queryProvider.Query<Report>().Any(r => r.TargetId == t.Id), Errors.ReportAlreadyExists);

        return await reportResult
            .Bind(_ => Report.Create(request.PersonalTipId, nameof(PersonalTip), request.Reason))
            .Tap(r => repository.Store(r));
    }
}
