using MediatR;
using System.Web;
using HealthCoach.Shared.Web;
using HealthCoach.Shared.Core;
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
        var query = HttpUtility.ParseQueryString(request.Url.Query);
        var emailAddress = query["EmailAddress"];

        var emailAddressResult = emailAddress.EnsureNotNullOrEmpty(BusinessErrors.User.Get.EmailAddressDoesntExist);

        return await emailAddressResult
            .Bind(ea => mediator.Send(new GetUserCommand(ea)))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}