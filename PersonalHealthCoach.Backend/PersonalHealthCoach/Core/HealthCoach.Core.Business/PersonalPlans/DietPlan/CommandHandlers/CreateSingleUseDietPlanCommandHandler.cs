using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.DietPlan.Create;

namespace HealthCoach.Core.Business;

internal sealed class CreateSingleUseDietPlanCommandHandler : IRequestHandler<CreateSingleUseDietPlanCommand, Result<DietPlan>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;
    private readonly IHttpClient httpClient;

    public CreateSingleUseDietPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
        httpClient = httpClientFactory
            .OnBaseUrl(ExternalEndpoints.Ai.BaseUrl)
            .OnRoute(ExternalEndpoints.Ai.DietPlanner);
    }

    public async Task<Result<DietPlan>> Handle(CreateSingleUseDietPlanCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);

        var dataResult = queryProvider
            .Query<PersonalData>()
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefault(e => e.UserId == request.UserId)
            .EnsureNotNull(Errors.PersonalDataNotFound);

        return await Result.FirstFailureOrSuccess(userResult, dataResult)
            .Map(() => new RequestDietPlanCommand(userResult.Value.Id.ToString(), new List<string>(), "", dataResult.Value!.Goal, "diet"))
            .Bind(async command => await httpClient.Post<RequestDietPlanCommand, RequestDietPlanCommandResponse>(command))
            .Bind(response => DietPlan.Create(request.UserId,
                response.diet.name,
                response.diet.use,
                response.diet.tag,
                response.diet.todo,
                response.diet.donot,
                ToMeal(response.breakfast),
                ToMeal(response.drink),
                ToMeal(response.mainCourse),
                ToMeal(response.sideDish),
                ToMeal(response.snack),
                ToMeal(response.soup)));
    }

    private static Meal ToMeal(DietPlannerApiResponseMeal dietMeal)
    {
        return new Meal(dietMeal.image, dietMeal.ingredients, dietMeal.kcal, dietMeal.title);
    }
}