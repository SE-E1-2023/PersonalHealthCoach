using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Create;

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
        this.httpClient = httpClientFactory.OnBaseUrl();
    }

    public async Task<Result<FitnessPlan>> Handle(CreateFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);

        throw new NotImplementedException();
    }
}