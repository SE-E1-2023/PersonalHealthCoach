using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class FoodHistoryFunctions
{
    private readonly IMediator mediator;

    public FoodHistoryFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(UpdateFoodHistory))]
    public async Task<HttpResponseData> UpdateFoodHistory([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/food-history")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<UpdateFoodHistory>()
            .Map(c => c with { UserId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }

    [Function(nameof(GetFoodHistory))]
    public async Task<HttpResponseData> GetFoodHistory([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/users/{id}/food-history")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new GetFoodHistoryCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}