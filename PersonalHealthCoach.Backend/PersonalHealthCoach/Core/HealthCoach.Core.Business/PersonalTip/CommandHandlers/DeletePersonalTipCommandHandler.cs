using MediatR;
using HealthCoach.Shared.Core;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalTip.Get;

namespace HealthCoach.Core.Business;

internal sealed class DeletePersonalTipCommandHandler : IRequestHandler<DeletePersonalTipCommand, Result>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public DeletePersonalTipCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result> Handle(DeletePersonalTipCommand request, CancellationToken cancellationToken)
    {
        var planResult = queryProvider
            .Query<PersonalTip>()
            .FirstOrDefault(p => p.Id == request.PersonalTipId)
            .EnsureNotNull(Errors.PersonalTipNotFound);

        return await planResult
            .Tap(p => repository.Delete(p!));
    }
}