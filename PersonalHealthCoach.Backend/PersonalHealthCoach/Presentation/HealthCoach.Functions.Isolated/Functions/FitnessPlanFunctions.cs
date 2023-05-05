using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class FitnessPlanFunctions
{
    private readonly IMediator mediator;

    public FitnessPlanFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(CreateFitnessPlan))]
    public async Task<HttpResponseData> CreateFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/user/{id}/plans/fitness")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new CreateFitnessPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(GetLatestFitnessPlan))]
    public async Task<HttpResponseData> GetLatestFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/user/{id}/plans/fitness/latest")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new GetLatestFitnessPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(DeleteFitnessPlan))]
    public async Task<HttpResponseData> DeleteFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Delete, Route = "v1/plans/fitness/{id}")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new DeleteFitnessPlanCommand(id))
            .ToResponseData(request);
    }
}