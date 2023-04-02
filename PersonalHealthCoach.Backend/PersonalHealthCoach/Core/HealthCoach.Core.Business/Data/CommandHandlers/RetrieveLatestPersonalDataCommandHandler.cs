using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using MediatR;
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
        var personalDataResult = queryProvider
            .Query<PersonalData>()
            .Where(pd => pd.UserId == request.UserId)
            .OrderByDescending(pd => pd.CreatedAt)
            .FirstOrDefault()
            .EnsureNotNull(Errors.PersonalDataNotFound);

        
        return personalDataResult;
    }
}