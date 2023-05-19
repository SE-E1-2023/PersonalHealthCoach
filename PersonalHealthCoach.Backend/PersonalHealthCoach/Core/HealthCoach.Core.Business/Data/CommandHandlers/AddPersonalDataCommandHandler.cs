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
                request.UnwantedExercises,
                request.DailySteps,
                request.HoursOfSleep,
                request.Gender,
                request.IsProUser,
                request.WorkoutsPerWeek,
                request.HasOther,
                request.HasMachine,
                request.HasBarbell,
                request.HasDumbbell,
                request.HasKettlebell,
                request.HasCable,
                request.HasEasyCurlBar,
                request.HasNone,
                request.HasBands,
                request.HasMedicineBall,
                request.HasExerciseBall,
                request.HasFoamRoll,
                request.WantsBodyOnly
            ))
            .Tap(p => repository.Store(p));
    }
}

