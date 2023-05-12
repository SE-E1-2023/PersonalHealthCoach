using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class ExerciseHistoryFunctions
{
    private readonly IMediator mediator;

    public ExerciseHistoryFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(UpdateExerciseHistory))]
    public async Task<HttpResponseData> UpdateExerciseHistory([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/exercise-history")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<UpdateExerciseHistoryCommand>()
            .Map(c => c with { UserId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }

    [Function(nameof(GetExerciseHistory))]
    public async Task<HttpResponseData> GetExerciseHistory([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/users/{id}/exercise-history")] HttpRequestData request, Guid id)
    {

        return await mediator
            .Send(new GetExerciseHistoryCommand(id))
            .ToResponseData(request , (response,result) => response.WriteAsJsonAsync(result.Value));
    }
}