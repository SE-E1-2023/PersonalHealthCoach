using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.PersonalData.Create;

namespace HealthCoach.Core.Business;

internal class AddPersonalDataCommandHandler : IRequestHandler<AddPersonalDataCommand, Result<PersonalData>>
{
    private readonly IRepository repository;

    public AddPersonalDataCommandHandler(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PersonalData>> Handle(AddPersonalDataCommand request, CancellationToken cancellationToken)
    {
        var userResult = await repository.Load<User>(request.UserId).ToResult(Errors.UserNotFound);

        return await userResult
            .Bind(_ => PersonalData.Create(
                request.UserId,
                request.DateOfBirth,
                request.Weight,
                request.Height,
                request.MedicalHistory,
                request.CurrentIllnesses,
                request.Goal,
                request.UnwantedExercises))
            .Tap(p => repository.Store(p));
    }
}

