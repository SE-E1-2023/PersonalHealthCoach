﻿using CSharpFunctionalExtensions;
using HealthCoach.Core.Business;
using HealthCoach.Shared.Web;
using MediatR;
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
    public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "users")] HttpRequestData request)
    {
        return await request.DeserializeBodyPayload<CreateUserCommand>()
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}