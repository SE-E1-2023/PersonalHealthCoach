using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public class PersonalTipFunctions
{
    private readonly IMediator mediator;

    public PersonalTipFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(CreatePersonalTip))]
    public async Task<HttpResponseData> CreatePersonalTip([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/plans/tips")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new CreatePersonalTipCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}