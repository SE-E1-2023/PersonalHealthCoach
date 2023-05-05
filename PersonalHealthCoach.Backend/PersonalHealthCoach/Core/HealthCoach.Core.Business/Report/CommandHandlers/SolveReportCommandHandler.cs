using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

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
        var callerResult = await repository
            .Load<User>(request.CallerId)
            .ToResult(Errors.UserNotFound)
            .Ensure(u => u.HasElevatedRights, Errors.UserNotAuthorized);
        
        var reportResult = await repository
            .Load<Report>(request.ReportId)
            .ToResult(Errors.ReportNotFound)
            .Ensure(r => r.SolvedAt is null, Errors.ReportAlreadySolved);

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

        return await Result.FirstFailureOrSuccess(callerResult, reportResult, destination)
            .Bind(() => RouteRequest(destination.Value, reportResult.Value.TargetId, callerResult.Value.Id))
            .Bind(() => reportResult.Value.Solve())
            .Tap(() => repository.Store(reportResult.Value));
    }

    private async Task<Result> RouteRequest(RequestType destination, Guid targetId, Guid callerId)
    {
        if (destination is RequestType.FitnessPlans)
            return await httpClient
                .OnRoute(InternalEndpoints.DeleteFitnessPlan)
                .WithHeaders(new Dictionary<string, string> { { "X-User-Id", callerId.ToString() } })
                .Delete(new DeleteFitnessPlanCommand(targetId, callerId));

        if (destination is RequestType.DietPlans)
            return await httpClient
                .OnRoute(InternalEndpoints.DeleteDietPlan)
                .WithHeaders(new Dictionary<string, string> { { "X-User-Id", callerId.ToString() } })
                .Delete(new DeleteDietPlanCommand(targetId, callerId));

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