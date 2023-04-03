using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalData.Get;

namespace HealthCoach.Core.Business;

public class RetrieveLatestPersonalDataCommandHandler : IRequestHandler<RetrieveLatestPersonalDataCommand, Result<PersonalData>>
{
    private readonly IEfQueryProvider queryProvider;
    
    public RetrieveLatestPersonalDataCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }
    
    public async Task<Result<PersonalData>> Handle(RetrieveLatestPersonalDataCommand request, CancellationToken cancellationToken)
    {
        return queryProvider
            .Query<PersonalData>()
            .Where(pd => pd.UserId == request.UserId)
            .OrderByDescending(pd => pd.CreatedAt)
            .FirstOrDefault()
            .EnsureNotNull(Errors.PersonalDataNotFound)!;
    }
}