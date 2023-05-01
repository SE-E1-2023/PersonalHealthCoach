
using CSharpFunctionalExtensions;
using HealthCoach.Core.Business;
using HealthCoach.Shared.Web;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public class DietPlanFunctions
{
    private readonly IMediator mediator;

    public DietPlanFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(ReportDietPlan))]

    public async Task<HttpResponseData> ReportDietPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/api/plans/diet/{id}/report")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<ReportDietPlanCommand>()
            .Map(c => c with { DietPlanId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }

}
