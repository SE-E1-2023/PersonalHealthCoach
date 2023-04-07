using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using MediatR;
using AIApi = HealthCoach.Shared.Web.ExternalEndpoints.AI;
using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalTip.Create;


namespace HealthCoach.Core.Business
{
    internal class CreatePersonalTipCommandHandler : IRequestHandler<CreatePersonalTipCommand, Result<PersonalTip>>
    {
        private readonly IRepository _repository;
        private readonly IEfQueryProvider _queryProvider;
        private readonly IHttpClient _httpClient;

        public CreatePersonalTipCommandHandler(IRepository repository, IEfQueryProvider efQueryProvider, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _queryProvider = efQueryProvider;
            _httpClient = httpClientFactory
                .OnBaseUrl(AIApi.BaseUrl)
                .OnRoute(AIApi.TipGenerator);
        }

        public async Task<Result<PersonalTip>> Handle(CreatePersonalTipCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);
             
            var dataResult = _queryProvider
                .Query<PersonalData>()
                .OrderByDescending(p => p.CreatedAt)
                .Where(p => p.UserId == request.UserId)
                .Select(p => p.Goal)
                .FirstOrDefault()
                .EnsureNotNull(Errors.PersonalDataNotFound);

            return await Result.FirstFailureOrSuccess(userResult, dataResult)
               .Map(() => new RequestPersonalTipCommand(dataResult.Value))
               .Bind(async command => await _httpClient.Post<RequestPersonalTipCommand, RequestPersonalTipCommandResponse>(command))
               .Bind(response => PersonalTip.Create(
                   request.UserId,
                   response.tip_type,
                   response.tip))
               .Tap(p => _repository.Store(p));

        }
    }
}
