﻿using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

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
        httpClient = httpClientFactory
            .OnBaseUrl(AIApi.BaseUrl)
            .OnRoute(AIApi.FitnessPlanner);
    }

    public async Task<Result<FitnessPlan>> Handle(CreateFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);
        var dataResult = queryProvider
            .Query<PersonalData>()
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefault(e => e.UserId == request.UserId)
            .EnsureNotNull(Errors.PersonalDataNotFound);

        var testPlans = queryProvider
            .Query<FitnessPlan>()
            .Where(p => p.UserId == request.UserId)
            .ToList();

        return await Result.FirstFailureOrSuccess(userResult, dataResult)
            .Map(() => new RequestFitnessPlanCommand(1))
            .Bind(async command => await httpClient.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(command))
            .Bind(response => FitnessPlan.Create(
                request.UserId,
                response.workout.workout.Select(e => Exercise.Create(e.exercise, e.rep_range, e.rest_time, e.sets, e.type)).ToList()))
            .Tap(p => repository.Store(p));
    }
}