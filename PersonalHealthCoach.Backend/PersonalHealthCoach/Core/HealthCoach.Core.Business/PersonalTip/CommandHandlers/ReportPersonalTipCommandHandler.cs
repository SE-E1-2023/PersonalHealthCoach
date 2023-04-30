using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;
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
        var tipResult = await repository.Load<PersonalTip>(request.PersonalTipId)
            .ToResult(Errors.PersonalTipDoesNotExist);

        var reportResult = tipResult
               .Map(_ => queryProvider.Query<Report>()
               .FirstOrDefault(e => e.TargetId == request.PersonalTipId))
               .Ensure(r => r == null, Errors.ReportAlreadyExists);

        return await reportResult
            .Bind(result=>Report.Create(
                request.PersonalTipId,
                nameof(PersonalTip),
                request.Reason))
            .Tap(r => repository.Store(r));
    }
}
