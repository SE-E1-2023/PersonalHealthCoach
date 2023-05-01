using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Functions.Isolated;

public sealed class GeneralWellnessTipFunctions
{
    private readonly IMediator mediator;

    public GeneralWellnessTipFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(GetRandomGeneralWellnessTip))]
    public async Task<HttpResponseData> GetRandomGeneralWellnessTip([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/tips/general")] HttpRequestData request)
    {
        return await mediator
            .Send(new GetRandomWellnessTipCommand())
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

}