using MediatR;
using HealthCoach.Shared.Core;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Delete;

namespace HealthCoach.Core.Business;

internal sealed class DeleteFitnessPlanCommandHandler : IRequestHandler<DeleteFitnessPlanCommand, Result>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public DeleteFitnessPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result> Handle(DeleteFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var callerResult = await repository
            .Load<User>(request.CallerId)
            .ToResult(Errors.UserNotFound)
            .Ensure(u => u.HasElevatedRights, Errors.UserNotAuthorized);

        var planResult = queryProvider
            .Query<FitnessPlan>()
            .FirstOrDefault(p => p.Id == request.FitnessPlanId)
            .EnsureNotNull(Errors.FitnessPlanNotFound);

        return await Result.FirstFailureOrSuccess(callerResult, planResult)
            .Tap(async () =>
            {
                foreach (var ex in planResult.Value!.Exercises)
                {
                    await repository.Delete(ex);
                }
            })
            .Tap(() => repository.Delete(planResult.Value!));
    }
}