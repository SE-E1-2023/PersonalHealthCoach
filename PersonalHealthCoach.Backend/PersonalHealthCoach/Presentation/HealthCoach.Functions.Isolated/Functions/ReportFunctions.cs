using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public class ReportFunctions
{
    private readonly IMediator mediator;

    public ReportFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(ReportPersonalTip))]
    public async Task<HttpResponseData> ReportPersonalTip([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/plans/tips/{id}/report")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<ReportPersonalTipCommand>()
            .Map(c => c with { PersonalTipId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }


    [Function(nameof(ReportDietPlan))]
    public async Task<HttpResponseData> ReportDietPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/plans/diet/{id}/report")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<ReportDietPlanCommand>()
            .Map(c => c with { DietPlanId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }

    [Function(nameof(ReportFitnessPlan))]
    public async Task<HttpResponseData> ReportFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/plans/fitness/{id}/report")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<ReportFitnessPlanCommand>()
            .Map(c => c with { FitnessPlanId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }

    [Function(nameof(SolveReport))]
    public async Task<HttpResponseData> SolveReport([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Patch, Route = "v1/reports/{id}/solve")] HttpRequestData request, Guid id)
    {
        var headerValue = request.Headers.GetValues("X-User-Id").FirstOrDefault();
        var userId = string.IsNullOrEmpty(headerValue)
            ? Guid.Empty 
            : Guid.Parse(headerValue);

        var command = new SolveReportCommand(id, userId);

        return await mediator
            .Send(command)
            .ToResponseData(request);
    }
}