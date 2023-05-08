using MediatR;
using HealthCoach.Shared.Core;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Get;

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
        var planResult = queryProvider
            .Query<FitnessPlan>()
            .FirstOrDefault(p => p.Id == request.FitnessPlanId)
            .EnsureNotNull(Errors.FitnessPlanNotFound);

        await planResult.Tap(async f =>
        {
            foreach (var ex in f!.Exercises)
            {
                await repository.Delete(ex);
            }
        });

        return await planResult
            .Tap(p => repository.Delete(p!));
    }
}