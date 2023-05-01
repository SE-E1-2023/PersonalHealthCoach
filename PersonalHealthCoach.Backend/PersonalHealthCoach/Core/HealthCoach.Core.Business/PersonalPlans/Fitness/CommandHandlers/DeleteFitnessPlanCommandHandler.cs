using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;
using HealthCoach.Shared.Web;
using HealthCoach.Core.Domain;
using MediatR;

using Errors = HealthCoach.Core.Business.BusinessErrors.FitnessPlan.Get;

namespace HealthCoach.Core.Business;

internal sealed class DeleteFitnessPlanCommandHandler : IRequestHandler<DeleteFitnessPlanCommand, Result<FitnessPlan>>
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public DeleteFitnessPlanCommandHandler(IRepository repository, IEfQueryProvider queryProvider, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task<Result<FitnessPlan>> Handle(DeleteFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        var planResult = await repository.Load<FitnessPlan>(request.PlanId).ToResult(Errors.FitnessPlanNotFound);
        //var exerciseList = planResult.Value.Exercises;
        //var exerciseList = await repository.Load<Exercise>(planResult.Value.Exercises).ToResult(planResult.Value.Exercises);
        //var exerciseList = queryProvider.Query<Exercise>().Where(ex => ex.FitnessPlanId == request.PlanId).ToArray();

        planResult.Tap(f =>
        {
            foreach (var ex in f.Exercises)
            {
                repository.Delete(ex);
            }
        });

        return await planResult.Tap(p => repository.Delete(p));

        //foreach (var item in planResult)
        //{
        //    await repository.Delete(item);
        //}

        //var exercises = planResult.Tap(r => r.Exercises.ToList());
        //Console.WriteLine(planResult.Value.Exercises);
        //foreach (var item in exercises)
        //{
        //    //await repository.Delete(item);
        //    Console.WriteLine(item.Name);
        //}
        //return null;

        //return await Result.FirstFailureOrSuccess(userResult, dataResult)
        //    .Map(() => new RequestFitnessPlanCommand(1))
        //    .Bind(async command => await httpClient.Post<RequestFitnessPlanCommand, RequestFitnessPlanCommandResponse>(command))
        //    .Bind(response => FitnessPlan.Create(
        //        request.UserId,
        //        response.workout.workout.Select(e => Exercise.Create(e.exercise, e.rep_range, e.rest_time, e.sets, e.type)).ToList()))
        //    .Tap(p => repository.Store(p));
    }
}