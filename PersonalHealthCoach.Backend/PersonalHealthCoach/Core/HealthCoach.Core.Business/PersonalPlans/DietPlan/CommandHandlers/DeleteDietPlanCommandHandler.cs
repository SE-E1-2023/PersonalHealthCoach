using MediatR;
using HealthCoach.Shared.Core;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.DietPlan.Delete;

namespace HealthCoach.Core.Business;

internal sealed class DeleteDietPlanCommandHandler : IRequestHandler<DeleteDietPlanCommand, Result>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public DeleteDietPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result> Handle(DeleteDietPlanCommand request, CancellationToken cancellationToken)
    {
        var planResult = queryProvider
            .Query<DietPlan>()
            .FirstOrDefault(p => p.Id == request.DietPlanId)
            .EnsureNotNull(Errors.DietPlanDoesNotExist);

        return await planResult
            .Tap(p => repository.Delete(p!));
    }
}