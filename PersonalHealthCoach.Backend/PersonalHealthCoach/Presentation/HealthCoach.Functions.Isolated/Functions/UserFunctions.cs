using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class UserFunctions
{
    private readonly IMediator mediator;

    public UserFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(CreateUser))]
    public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users")] HttpRequestData request)
    {
        return await request.DeserializeBodyPayload<CreateUserCommand>()
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(GetUserByEmailAddress))]
    public async Task<HttpResponseData> GetUserByEmailAddress([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/users")] HttpRequestData request)
    {
        return await request.DeserializeBodyPayload<GetUserCommand>()
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}