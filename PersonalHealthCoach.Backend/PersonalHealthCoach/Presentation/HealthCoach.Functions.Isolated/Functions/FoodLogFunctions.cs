using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class FoodLogFunctions
{
    private readonly IMediator mediator;

    public FoodLogFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(UpdateFoodLog))]
    public async Task<HttpResponseData> UpdateFoodLog([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/food-log")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<UpdateFoodLogCommand>()
            .Map(c => c with { UserId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }
}