using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Create;
using AIApi = HealthCoach.Shared.Web.ExternalEndpoints.AI;

namespace HealthCoach.Core.Business;

internal sealed class CreateFitnessPlanCommandHandler : IRequestHandler<CreateFitnessPlanCommand, Result<FitnessPlan>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;
    private readonly IHttpClient httpClient;

    public CreateFitnessPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
        this.httpClient = httpClientFactory
            .OnBaseUrl(AIApi.BaseUrl)
            .OnRoute(AIApi.FitnessPlanner);
    }

    public async Task<Result<FitnessPlan>> Handle(CreateFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);

        return await userResult
            .Map(_ => new RequestFitnessPlanCommand(1))
            .Bind(async command => await httpClient.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(command))
            // .Bind(c => new RequestFitnessPlanCommandResponse)
            .Bind(response => FitnessPlan.Create(
                request.UserId,
                response.workout.Select(e => Exercise.Create(e.exercise, e.rep_range, e.rest_time, e.sets, e.type)).ToList()))
            .Tap(p => repository.Store(p));
    }
}