using MediatR;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Routes = HealthCoach.Shared.Web.ExternalEndpoints.AI;
using Errors = HealthCoach.Core.Business.BusinessErrors.WellnessPlan.Create;

namespace HealthCoach.Core.Business;

internal sealed class CreateWellnessPlanCommandHandler : IRequestHandler<CreateWellnessPlanCommand, Result<CreateWellnessPlanCommandResponse>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;
    private readonly IHttpClient httpClient;

    public CreateWellnessPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
        httpClient = httpClientFactory.OnBaseUrl(Routes.BaseUrl).OnRoute(Routes.Wellness);
    }

    public async Task<Result<CreateWellnessPlanCommandResponse>> Handle(CreateWellnessPlanCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);

        return await userResult
            .Map(_ => queryProvider.Query<PersonalData>().OrderByDescending(p => p.CreatedAt).FirstOrDefault(e => e.UserId == request.UserId))
            .Map(d => new RequestWellnessPlanCommand(d == null ? new List<string>() : d.CurrentIllnesses))
            .Bind(async c => await httpClient.Post<RequestWellnessPlanCommand, RequestWellnessPlanCommandResponse>(c))
            .Map(r => new CreateWellnessPlanCommandResponse(r.Action.Title, r.Action.Description, r.Categories));
    }
}