using CSharpFunctionalExtensions;
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
    public async Task<HttpResponseData> CreateFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/plans/fitness")] HttpRequestData request, Guid id)
    {
        return await request
            .DeserializeBodyPayload<CreateFitnessPlanCommand>()
            .Map(c => c with { UserId = id })
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
    
    [Function(nameof(CreateSingleUseFitnessPlan))]
    public async Task<HttpResponseData> CreateSingleUseFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/plans/fitness/single-use")] HttpRequestData request, Guid id)
    {
        return await request
            .DeserializeBodyPayload<CreateFitnessPlanCommand>()
            .Map(c => c with { UserId = id })
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(GetLatestFitnessPlan))]
    public async Task<HttpResponseData> GetLatestFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/users/{id}/plans/fitness/latest")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new GetLatestFitnessPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(DeleteFitnessPlan))]
    public async Task<HttpResponseData> DeleteFitnessPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Delete, Route = "v1/plans/fitness/{id}")] HttpRequestData request, Guid id)
    {
        var headerValue = request.Headers.GetValues("X-User-Id").FirstOrDefault();
        var userId = string.IsNullOrEmpty(headerValue)
            ? Guid.Empty
            : Guid.Parse(headerValue);

        return await mediator
            .Send(new DeleteFitnessPlanCommand(id, userId))
            .ToResponseData(request);
    }
}