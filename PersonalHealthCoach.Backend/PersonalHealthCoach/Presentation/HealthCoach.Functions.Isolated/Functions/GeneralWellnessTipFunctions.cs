using HealthCoach.Core.Business;
using HealthCoach.Shared.Web;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using HealthCoach.Core.Domain;

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
        return await mediator.Send(new GetRandomWellnessTipCommand())
            .ToResponseData(request, (response, result) => response.WriteAsJsonAsync(result.Value));
    }

}