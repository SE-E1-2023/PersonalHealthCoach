using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Functions.Isolated;

public class DietPlanFunctions
{
    private readonly IMediator mediator;
    private readonly IEfQueryProvider queryProvider;

    public DietPlanFunctions(IMediator mediator, IEfQueryProvider queryProvider)
    {
        this.mediator = mediator;
        this.queryProvider = queryProvider;
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

    [Function(nameof(CreateDietPlan))]
    public async Task<HttpResponseData> CreateDietPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/plans/diet")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new CreateDietPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(GetDietPlan))]
    public async Task<HttpResponseData> GetDietPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/users/{id}/plans/diet")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new GetDietPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(CreateSingleUseDietPlan))]
    public async Task<HttpResponseData> CreateSingleUseDietPlan([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/plans/diet/single-use")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new CreateDietPlanCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}

