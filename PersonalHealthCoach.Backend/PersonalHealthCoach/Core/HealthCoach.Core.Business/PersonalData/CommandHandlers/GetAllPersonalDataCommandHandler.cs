using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;
using System.Linq;

using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalData.Get;

namespace HealthCoach.Core.Business;

public class GetAllPersonalDataCommandHandler : IRequestHandler<GetAllPersonalDataCommand, Result<PersonalData>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetAllPersonalDataCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public async Task<Result<PersonalData>> Handle(GetAllPersonalDataCommand request, CancellationToken cancellationToken)
    {
        var user = queryProvider.Query<PersonalData>().FirstOrDefault(u => u.UserId == request.id);

        return user is not null ? user : Result.Failure<PersonalData>(Errors.UserIdNotFound);
    }
}
