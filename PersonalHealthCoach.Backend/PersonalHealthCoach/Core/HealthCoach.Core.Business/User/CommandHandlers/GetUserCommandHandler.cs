using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        var user = queryProvider.Query<User>().FirstOrDefault(u => u.EmailAddress == request.EmailAddress);

        return user is not null ? Result.Success(user.Id) : Result.Failure<Guid>(Errors.EmailAddressDoesntExist);

    }
}
