using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;
using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Business;

using Errors = HealthCoach.Core.Business.BusinessErrors.ExerciseHistory.GetExercices;

public class GetExerciseHistoryCommandHandler : IRequestHandler<GetExerciseHistoryCommand, Result<ExerciseHistory> >
{
    private readonly IEfQueryProvider queryProvider;

    public GetExerciseHistoryCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public Task<Result<ExerciseHistory> > Handle(GetExerciseHistoryCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(queryProvider
               .Query<ExerciseHistory>()
               .FirstOrDefault(log => log.Id == request.UserId)!
               .EnsureNotNull(Errors.LogIsEmpty));
    }
}
