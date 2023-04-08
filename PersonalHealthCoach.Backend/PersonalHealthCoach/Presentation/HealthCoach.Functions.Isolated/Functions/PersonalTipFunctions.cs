using HealthCoach.Core.Business;
using HealthCoach.Shared.Web;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;

namespace HealthCoach.Functions.Isolated;

public class PersonalTipFunctions
{
    private readonly IMediator mediator;

    public PersonalTipFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(CreatePersonalTip))]
    public async Task<HttpResponseData> CreatePersonalTip([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/api/users/{id}/plans/tips")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new CreatePersonalTipCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}

