using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;
using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Business;

using Errors = HealthCoach.Core.Business.BusinessErrors.ExerciseLog.GetExercices;

public class GetExerciseLogCommandHandler : IRequestHandler<GetExerciseLogCommand, Result<ExerciseLog> >
{
    private readonly IEfQueryProvider queryProvider;

    public GetExerciseLogCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }

    public Task<Result<ExerciseLog> > Handle(GetExerciseLogCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(queryProvider
               .Query<ExerciseLog>()
               .FirstOrDefault(log => log.Id == request.UserId)!
               .EnsureNotNull(Errors.LogIsEmpty));
    }
}
