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

    [Function(nameof(DeleteDietPlan))]
    public async Task<HttpResponseData> DeleteDietPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Delete, Route = "v1/plans/diet/{id}")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new DeleteDietPlanCommand(id))
            .ToResponseData(request);
    }
}

