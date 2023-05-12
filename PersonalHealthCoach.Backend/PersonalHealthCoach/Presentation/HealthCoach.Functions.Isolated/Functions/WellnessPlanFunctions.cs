using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class WellnessPlanFunctions
{
    private readonly IMediator mediator;

    public WellnessPlanFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(RequestWellnessPlan))]
    public async Task<HttpResponseData> RequestWellnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/plans/wellness")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new CreateWellnessPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}