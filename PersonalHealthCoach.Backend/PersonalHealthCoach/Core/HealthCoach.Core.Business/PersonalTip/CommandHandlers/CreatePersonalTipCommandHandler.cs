using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Web;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalTip.Create;


namespace HealthCoach.Core.Business;

internal class CreatePersonalTipCommandHandler : IRequestHandler<CreatePersonalTipCommand, Result<PersonalTip>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;
    private readonly IHttpClient httpClient;
    private readonly IFoodHistoryRepository foodHistoryRepository;
    private readonly IExerciseHistoryRepository exerciseHistoryRepository;

    public CreatePersonalTipCommandHandler(IRepository repository, IEfQueryProvider queryProvider, IHttpClientFactory httpClientFactory, IFoodHistoryRepository foodHistoryRepository, IExerciseHistoryRepository exerciseHistoryRepository)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
        this.foodHistoryRepository = foodHistoryRepository;
        this.exerciseHistoryRepository = exerciseHistoryRepository;
        this.httpClient = httpClientFactory
            .OnBaseUrl(ExternalEndpoints.Ai.BaseUrl)
            .OnRoute(ExternalEndpoints.Ai.TipGenerator);
    }

    public async Task<Result<PersonalTip>> Handle(CreatePersonalTipCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);
        if (userResult.IsFailure)
        {
            return userResult.ConvertFailure<PersonalTip>();
        }

        var dataResult = userResult
                .Map(_ => queryProvider.Query<PersonalData>()
                    .OrderByDescending(p => p.CreatedAt)
                    .FirstOrDefault(e => e.UserId == request.UserId))
                .EnsureNotNull(Errors.PersonalDataNotFound);
        if (dataResult.IsFailure)
        {
            return dataResult.ConvertFailure<PersonalTip>();
        }

        var lastSevenData = userResult
            .Map(_ => queryProvider.Query<PersonalData>()
                           .OrderByDescending(p => p.CreatedAt)
                           .Where(e => e.UserId == request.UserId)
                           .Take(7)
                           .ToList());

        var foodHistoryResult = userResult.Map(u => queryProvider.Query<FoodHistory>().FirstOrDefault(h => h.Id == u.Id));
        var exerciseHistoryResult = userResult.Map(u => queryProvider.Query<ExerciseHistory>().FirstOrDefault(h => h.Id == u.Id));

        var user = userResult.Value;
        var counter = 0;
        var requestProgress = new List<RequestProgress>();

        foreach (var data in lastSevenData.Value)
        {
            counter++;
            var foodHistory = new List<RequestFoodHistory>();
            var exerciseHistory = new List<RequestExerciseHistory>();

            if (foodHistoryResult.Value is not null)
            {
                foodHistory = foodHistoryResult.Value.ConsumedFoods.Where(f => f.ConsumedAt.Date == data.CreatedAt.Date).Select(f => new RequestFoodHistory(f.Title, f.Meal, f.Calories, f.Quantity)).ToList();
            }

            if (exerciseHistoryResult.Value is not null)
            {
                exerciseHistory = exerciseHistoryResult.Value.CompletedExercises.Where(e => e.CompletedAt.Date == data.CreatedAt.Date).Select(e => new RequestExerciseHistory(e.Title, e.DurationInMinutes, e.CaloriesBurned)).ToList();
            }

            requestProgress.Add(new(
                "Day " + counter,
                data.CreatedAt.ToString("yyyy-MM-dd"),
                (int)data.Weight,
                data.Goal,
                foodHistory.ToList(),
                exerciseHistory.ToList(),
                (int)data.DailySteps,
                (int)data.HoursOfSleep
            ));
        }
        var apiRequest = new RequestPersonalTipCommand(
            new(
                user.FirstName + user.Name,
                user.Id.ToString(),
                (TimeProvider.Instance().UtcNow - dataResult.Value.DateOfBirth).Days / 365,
                (int)dataResult.Value.Height,
                (int)dataResult.Value.Weight,
                dataResult.Value.Gender,
                dataResult.Value.Goal,
                "Moderately active",
                10000
            ),
            requestProgress
        );

        return await dataResult
            .Map(d => apiRequest)
            .Bind(async command =>
            {
                var httpResult = new Result<RequestPersonalTipCommandResponse>();
                for (var i = 0; i < 3; i++)
                {
                    httpResult = await httpClient.Post<RequestPersonalTipCommand, RequestPersonalTipCommandResponse>(command);
                    if (httpResult.IsSuccess)
                    {
                        break;
                    }
                }
                return httpResult;
            })
            .Bind(response => PersonalTip.Create(userResult.Value.Id, response.Type, response.Tip))
            .Tap(p => repository.Store(p));
    }
}

