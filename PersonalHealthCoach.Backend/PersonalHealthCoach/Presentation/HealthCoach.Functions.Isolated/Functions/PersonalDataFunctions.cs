using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Business;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HealthCoach.Functions.Isolated;

public sealed class PersonalDataFunctions
{
    private readonly IMediator mediator;

    public PersonalDataFunctions(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Function(nameof(AddPersonalData))]
    public async Task<HttpResponseData> AddPersonalData([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/data/personal")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<AddPersonalDataCommand>()
            .Map(c => c with { UserId = id });

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(GetAllPersonalData))]
    public async Task<HttpResponseData> GetAllPersonalData([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/users/{id}/data/personal")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<GetAllPersonalDataCommand>()
            .Map(c => new GetAllPersonalDataCommand(id));

        return await command
            .Bind(c => mediator.Send(c))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

    [Function(nameof(RetrieveLatestPersonalData))]
    public async Task<HttpResponseData> RetrieveLatestPersonalData([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Get, Route = "v1/user/{id}/data/personal/latest")] HttpRequestData request, Guid id)
    {
        return await mediator.Send(new RetrieveLatestPersonalDataCommand(id))
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }
}

