using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
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
        var headerValue = request.Headers.GetValues("X-User-Id").FirstOrDefault();
        var userId = string.IsNullOrEmpty(headerValue)
            ? Guid.Empty
            : Guid.Parse(headerValue);

        return await mediator
            .Send(new DeleteDietPlanCommand(id, userId))
            .ToResponseData(request);
    }
}

