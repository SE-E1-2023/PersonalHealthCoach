using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Create;
using AIApi = HealthCoach.Shared.Web.ExternalEndpoints.Ai;
using FitnessExercise = HealthCoach.Core.Domain.Exercise;

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
            .OnBaseUrl(ExternalEndpoints.Ai.BaseUrl)
            .OnRoute(ExternalEndpoints.Ai.FitnessPlanner);
    }

    public async Task<Result<FitnessPlan>> Handle(CreateFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);
        
        if(userResult.IsFailure)
        {
            return userResult.ConvertFailure<FitnessPlan>();
        }

        var dataResult = queryProvider
            .Query<PersonalData>()
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefault(e => e.UserId == request.UserId)
            .EnsureNotNull(Errors.PersonalDataNotFound);

        if(dataResult.IsFailure)
        {
            return dataResult.ConvertFailure<FitnessPlan>();
        }

        return await Result.FirstFailureOrSuccess(userResult, dataResult)
            .Map(() => new RequestFitnessPlanCommand(userResult.Value.Id.ToString(),
                dataResult.Value.IsProUser,
                dataResult.Value.Goal,
                dataResult.Value.WorkoutsPerWeek,
                request.FitnessScore,
                new RequestExercises(dataResult.Value.HasOther,
                    dataResult.Value.HasMachine,
                    dataResult.Value.HasBarbell,
                    dataResult.Value.HasDumbbell,
                    dataResult.Value.HasKettlebells,
                    dataResult.Value.HasCable,
                    dataResult.Value.HasEasyCurlBar,
                    dataResult.Value.HasNone,
                    dataResult.Value.HasBands,
                    dataResult.Value.HasMedicineBall,
                    dataResult.Value.HasExerciseBall,
                    dataResult.Value.HasFoamRoll,
                    dataResult.Value.WantsBodyOnly
                    )
                )
            )
            .Bind(async command => await httpClient.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(command))
            .Bind(response =>
            {
                var workout1 = new Workout();
                var workout2 = new Workout();
                var workout3 = new Workout();
                var workout4 = new Workout();
                var workout5 = new Workout();
                var workout6 = new Workout();
                var workout7 = new Workout();

                if (response.workouts.workout1 is not null)
                {
                    workout1 = new Workout(response.workouts.workout1);
                }
                
                if (response.workouts.workout2 is not null)
                {
                    workout2 = new Workout(response.workouts.workout2);
                }

                if (response.workouts.workout3 is not null)
                {
                    workout3 = new Workout(response.workouts.workout3);
                }

                if (response.workouts.workout4 is not null)
                {
                    workout4 = new Workout(response.workouts.workout4);
                }

                if (response.workouts.workout5 is not null)
                {
                    workout5 = new Workout(response.workouts.workout5);
                }

                if (response.workouts.workout6 is not null)
                {
                    workout6 = new Workout(response.workouts.workout6);
                }

                if (response.workouts.workout7 is not null)
                {
                    workout7 = new Workout(response.workouts.workout7);
                }

                return FitnessPlan.Create(request.UserId, new List<Workout> { workout1, workout2, workout3, workout4, workout5, workout6, workout7 });
            })
            .Tap(p => repository.Store(p));
    }
}