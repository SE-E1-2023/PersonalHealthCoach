using HealthCoach.Core.Business;
using HealthCoach.Shared.Web;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using CSharpFunctionalExtensions;

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

    [Function(nameof(ReportPersonalTip))]
    public async Task<HttpResponseData> ReportPersonalTip([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/api/plans/tips/{id}/report")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<ReportPersonalTipCommand>()
            .Map(c => c with { PersonalTipId = id });
       
        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request);
    }

    [Function(nameof(DeletePersonalTip))]
    public async Task<HttpResponseData> DeletePersonalTip([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Delete, Route = "v1/api/plans/tips/{id}")] HttpRequestData request, Guid id)
    {
        return await mediator
            .Send(new DeletePersonalTipCommand(id))
            .ToResponseData(request);
    }
}