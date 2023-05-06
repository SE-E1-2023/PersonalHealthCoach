using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

using Errors = HealthCoach.Core.Domain.DomainErrors.PersonalData.Create;

namespace HealthCoach.Core.Domain;

public class PersonalData : AggregateRoot
{
    public PersonalData() { }

    private PersonalData(
        Guid userId,
        DateTime? dateOfBirth,
        float weight, 
        float height, 
        List<string> medicalHistory,
        List<string> currentIllnesses,
        string goal,
        List<string> unwantedExercises,
        int? dailySteps,
        double? hoursOfSleep,
        string gender)
    {
        UserId = userId;
        DateOfBirth = (DateTime)dateOfBirth;
        Weight = weight;
        Height = height;
        MedicalHistory = medicalHistory ?? new List<string>();
        CurrentIllnesses = currentIllnesses ?? new List<string>();
        Goal = goal;
        UnwantedExercises = unwantedExercises ?? new List<string>();
        DailySteps = dailySteps;
        HoursOfSleep = hoursOfSleep;
        Gender = gender;
        CreatedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<PersonalData> Create(
        Guid userId,
        DateTime? dateOfBirth,
        float weight,
        float height,
        List<string> medicalHistory,
        List<string> currentIllnesses,
        string goal,
        List<string> unwantedExercises,
        int? dailySteps,
        double? hoursOfSleep,
        string gender)
    {
        var dateOfBirthResult = dateOfBirth
            .EnsureNotNull(Errors.DateOfBirthNull)
            .Ensure(d => d <= PersonalDataConstants.MinimumDateOfBirth, Errors.UserNotOldEnough);

        var weightResult = Result.SuccessIf(weight > 0, Errors.InvalidWeight);
        var heightResult = Result.SuccessIf(height > 0, Errors.InvalidHeight);

        var goalResult = goal
            .EnsureNotNullOrEmpty(Errors.GoalIsNullOrEmpty)
            .Ensure(g => PersonalDataConstants.AllowedGoals.Any(a => a.Equals(g)), Errors.GoalIsUnrecognized);

        var dailyStepsResult = Result.SuccessIf(dailySteps is >= 0 or null, Errors.InvalidDailySteps);
        var hoursOfSleepResult = Result.SuccessIf(hoursOfSleep is >= 0 or null, Errors.InvalidHoursOfSleep);

        var genderResult = gender
            .EnsureNotNullOrEmpty(Errors.InvalidGender)
            .Ensure(g => PersonalDataConstants.AllowedGenders.Any(a => a.Equals(g)), Errors.InvalidGender);

        return Result.FirstFailureOrSuccess(dateOfBirthResult, weightResult, heightResult, goalResult, dailyStepsResult, hoursOfSleepResult, genderResult)
            .Map(() => new PersonalData(userId, dateOfBirth, weight, height, medicalHistory, currentIllnesses, goal, unwantedExercises, dailySteps, hoursOfSleep, gender));
    }

    public Guid UserId { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public float Weight { get; private set; }

    public float Height { get; private set; }

    public List<string> MedicalHistory { get; private set; }

    public List<string> CurrentIllnesses { get; private set; }

    public string Goal { get; private set; }

    public List<string> UnwantedExercises { get; private set; }

    public int? DailySteps { get; private set; }

    public double? HoursOfSleep { get; private set; }

    public string Gender { get; private set; }

    public DateTime CreatedAt { get; private set; }
}

