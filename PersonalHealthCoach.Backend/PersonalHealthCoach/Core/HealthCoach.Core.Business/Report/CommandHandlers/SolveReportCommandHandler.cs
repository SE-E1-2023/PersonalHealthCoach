using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.Report.Solve;

namespace HealthCoach.Core.Business;

internal class SolveReportCommandHandler : IRequestHandler<SolveReportCommand, Result>
{
    private readonly IRepository repository;
    private readonly IHttpClient httpClient;

    public SolveReportCommandHandler(IRepository repository, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        httpClient = httpClientFactory.OnBaseUrl(InternalEndpoints.BaseUrl);
    }
    
    public async Task<Result> Handle(SolveReportCommand request, CancellationToken cancellationToken)
    {
        var reportResult = await repository
            .Load<Report>(request.ReportId)
            .ToResult(Errors.ReportNotFound)
            .Ensure(r => r.SolvedAt is null, Errors.ReportAlreadySolved);
        
        var callerResult = await repository.Load<User>(request.CallerId).ToResult(Errors.UserNotFound);
        
        var destination = reportResult.Map(r =>
        {
            return r.Target switch
            {
                nameof(FitnessPlan) => RequestType.FitnessPlans,
                nameof(DietPlan) => RequestType.DietPlans,
                nameof(PersonalTip) => RequestType.PersonalTips,
                _ => RequestType.Invalid
            };
        });

        return await Result.FirstFailureOrSuccess(reportResult, callerResult, destination)
            .Bind(() => RouteRequest(request, destination.Value, reportResult.Value.TargetId))
            .Bind(() => reportResult.Value.Solve())
            .Tap(() => repository.Store(reportResult.Value));
    }

    private async Task<Result> RouteRequest(SolveReportCommand request, RequestType destination, Guid targetId)
    {
        if (destination == RequestType.FitnessPlans)
            return await httpClient
                .OnRoute(InternalEndpoints.DeleteFitnessPlan)
                .Delete(new DeleteFitnessPlanCommand(targetId));

        if (destination == RequestType.DietPlans)
            return await httpClient
                .OnRoute(InternalEndpoints.DeleteDietPlan)
                .Delete(new DeleteDietPlanCommand(targetId));

        // if (destination == RequestType.PersonalTips)
        //     return await httpClient
        //         .OnRoute(InternalEndpoints.DeletePersonalTip)
        //         .Delete(new DeletePersonalTipCommand(targetId));
        
        return Result.Failure(Errors.InvalidReportType);
    }

    private enum RequestType
    {
        FitnessPlans,
        DietPlans,
        PersonalTips,
        Invalid
    }
}