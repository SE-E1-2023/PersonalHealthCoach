using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.User.Create;

namespace HealthCoach.Core.Business;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public CreateUserCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var users = queryProvider.Query<User>().Where(u => u.EmailAddress == request.EmailAddress);
        var duplicateResult = Result.FailureIf(users.Any(), Errors.EmailAddressAlreadyInUse);

        return await duplicateResult
            .Bind(() => User.Create(request.Name, request.FirstName, request.EmailAddress))
            .Tap(u => repository.Store(u));
    }
}