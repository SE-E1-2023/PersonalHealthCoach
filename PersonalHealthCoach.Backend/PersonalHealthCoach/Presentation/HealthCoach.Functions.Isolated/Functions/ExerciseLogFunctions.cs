using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class ExerciseLogFunctions
{
    private readonly IMediator mediator;

    public ExerciseLogFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(UpdateExerciseLog))]
    public async Task<HttpResponseData> UpdateExerciseLog([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/exercise-log")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<UpdateExerciseLogCommand>()
            .Map(c => c with { UserId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }
}