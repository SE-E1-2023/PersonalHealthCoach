﻿
using CSharpFunctionalExtensions;
using HealthCoach.Core.Business;
using HealthCoach.Shared.Web;
using MediatR;
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
    public async Task<HttpResponseData> AddPersonalData([HttpTrigger(AuthorizationLevel.Function, HttpVerbs.Post, Route = "v1/users/{id}/personal-data")] HttpRequestData request, Guid id)
    {
        var command = await request
            .DeserializeBodyPayload<AddPersonalDataCommand>()
            .Map(c => new AddPersonalDataCommand(id,
                c.DateOfBirth,
                c.Weight,
                c.Height,
                c.MedicalHistory,
                c.CurrentIllnesses,
                c.Goal,
                c.UnwantedExercises));

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
}

