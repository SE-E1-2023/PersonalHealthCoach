using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;
using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalData.Get;
using Microsoft.EntityFrameworkCore;

namespace HealthCoach.Core.Business.Data.CommandHandlers
{
    public class RetrieveLatestPersonalDataCommandHandler : IRequestHandler<RetrieveLatestPersonalDataCommand, Result<PersonalData>>
    {
        private readonly IEfQueryProvider _queryProvider;
        public RetrieveLatestPersonalDataCommandHandler(IEfQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }
        public async Task<Result<PersonalData>> Handle(RetrieveLatestPersonalDataCommand request, CancellationToken cancellationToken)
        {
            var result = _queryProvider.Query<PersonalData>().Where(pd => pd.UserId == request.id).OrderByDescending(pd => pd.CreatedAt).FirstOrDefault();

            if (result == null)
            {
                return Result.Failure<PersonalData>(Errors.PersonalDataNotFound);
            }
            return Result.Success(result);

        }
    }
}
