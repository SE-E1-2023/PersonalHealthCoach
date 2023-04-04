using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalData.Get;

namespace HealthCoach.Core.Business;

public class GetAllPersonalDataCommandHandler : IRequestHandler<GetAllPersonalDataCommand, Result<List<PersonalData>>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public GetAllPersonalDataCommandHandler(IEfQueryProvider queryProvider, IRepository repository)
    {
        this.queryProvider = queryProvider;
        this.repository = repository;
    }

    public async Task<Result<List<PersonalData>>> Handle(GetAllPersonalDataCommand request, CancellationToken cancellationToken)
    {
        //var user = queryProvider.Query<PersonalData>().FirstOrDefault(u => u.UserId == request.UserId);
        //var user = queryProvider.Query<PersonalData>().Where(p => p.UserId == request.UserId).OrderByDescending(p => p.CreatedAt).ToList();
        var user = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);
        var data = user
            .Map(u => queryProvider.Query<PersonalData>()
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList())
            .Ensure(p => p.Any(), Errors.PersonalDataNotFound);

        return data;

        //return user is not null ? user : Result.Failure<List<PersonalData>>(Errors.UserNotFound);
    }
}
