using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using MediatR;

using AIApi = HealthCoach.Shared.Web.ExternalEndpoints.AI;
using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalTip.Create;


namespace HealthCoach.Core.Business;

internal class CreatePersonalTipCommandHandler : IRequestHandler<CreatePersonalTipCommand, Result<PersonalTip>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;
    private readonly IHttpClient httpClient;

    public CreatePersonalTipCommandHandler(IRepository repository, IEfQueryProvider queryProvider, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
        this.httpClient = httpClientFactory
            .OnBaseUrl(AIApi.BaseUrl)
            .OnRoute(AIApi.TipGenerator);
    }

    public async Task<Result<PersonalTip>> Handle(CreatePersonalTipCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);

        var dataResult = userResult
                .Map(_ => queryProvider.Query<PersonalData>()
                    .OrderByDescending(p => p.CreatedAt)
                    .FirstOrDefault(e => e.UserId == request.UserId))
                .EnsureNotNull(Errors.PersonalDataNotFound);

        return await dataResult
            .Map(d => new RequestPersonalTipCommand(d.Goal))
            .Bind(async command => await httpClient.Post<RequestPersonalTipCommand, RequestPersonalTipCommandResponse>(command))
            .Bind(response => PersonalTip.Create(
                request.UserId,
                response.tip_type,
                response.tip))
            .Tap(p => repository.Store(p));
    }
}

