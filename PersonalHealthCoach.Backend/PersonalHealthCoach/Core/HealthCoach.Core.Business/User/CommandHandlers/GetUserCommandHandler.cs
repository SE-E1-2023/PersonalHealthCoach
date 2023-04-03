using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.User.Get;

namespace HealthCoach.Core.Business;

public class GetUserCommandHandler : IRequestHandler<GetUserCommand, Result<Guid>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetUserCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public async Task<Result<Guid>> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        return queryProvider
            .Query<User>()
            .FirstOrDefault(u => u.EmailAddress == request.EmailAddress)
            .EnsureNotNull(Errors.EmailAddressDoesntExist)
            .Map(u => u.Id);
    }
}
