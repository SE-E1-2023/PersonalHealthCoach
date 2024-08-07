﻿using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

using Errors = HealthCoach.Core.Domain.DomainErrors.PersonalData.Create;

namespace HealthCoach.Core.Domain;

public class PersonalData : AggregateRoot
{
    public PersonalData()
    {
    }

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
        string gender,
        bool isProUser,
        int workoutsPerWeek,
        bool hasOther,
        bool hasMachine,
        bool hasBarbell,
        bool hasDumbbell,
        bool hasKettlebell,
        bool hasCable,
        bool hasEasyCurlBar,
        bool hasNone,
        bool hasBands,
        bool hasMedicineBall,
        bool hasExerciseBall,
        bool hasFoamRoll,
        bool wantsBodyOnly
    )
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

        IsProUser = isProUser;
        WorkoutsPerWeek = workoutsPerWeek;
        HasOther = hasOther;
        HasMachine = hasMachine;
        HasBarbell = hasBarbell;
        HasDumbbell = hasDumbbell;
        HasKettlebells = hasKettlebell;
        HasCable = hasCable;
        HasEasyCurlBar = hasEasyCurlBar;
        HasNone = hasNone;
        HasBands = hasBands;
        HasMedicineBall = hasMedicineBall;
        HasExerciseBall = hasExerciseBall;
        HasFoamRoll = hasFoamRoll;
        WantsBodyOnly = wantsBodyOnly;
    }

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
        string gender,
        DateTime createdAt,

        bool isProUser,
        int workoutsPerWeek,
        bool hasOther,
        bool hasMachine,
        bool hasBarbell,
        bool hasDumbbell,
        bool hasKettlebell,
        bool hasCable,
        bool hasEasyCurlBar,
        bool hasNone,
        bool hasBands,
        bool hasMedicineBall,
        bool hasExerciseBall,
        bool hasFoamRoll,
        bool wantsBodyOnly
    )
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
        CreatedAt = createdAt;

        IsProUser = isProUser;
        WorkoutsPerWeek = workoutsPerWeek;
        HasOther = hasOther;
        HasMachine = hasMachine;
        HasBarbell = hasBarbell;
        HasDumbbell = hasDumbbell;
        HasKettlebells = hasKettlebell;
        HasCable = hasCable;
        HasEasyCurlBar = hasEasyCurlBar;
        HasNone = hasNone;
        HasBands = hasBands;
        HasMedicineBall = hasMedicineBall;
        HasExerciseBall = hasExerciseBall;
        HasFoamRoll = hasFoamRoll;
        WantsBodyOnly = wantsBodyOnly;
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
        string gender,
        bool isProUser,
        int workoutsPerWeek,
        bool hasOther,
        bool hasMachine,
        bool hasBarbell,
        bool hasDumbbell,
        bool hasKettlebell,
        bool hasCable,
        bool hasEasyCurlBar,
        bool hasNone,
        bool hasBands,
        bool hasMedicineBall,
        bool hasExerciseBall,
        bool hasFoamRoll,
        bool wantsBodyOnly
    )
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

        return Result.FirstFailureOrSuccess(dateOfBirthResult, weightResult, heightResult, goalResult, dailyStepsResult,
                hoursOfSleepResult, genderResult)
            .Map(() => new PersonalData(userId, dateOfBirth, weight, height, medicalHistory, currentIllnesses, goal,
                unwantedExercises, dailySteps, hoursOfSleep, gender, isProUser, workoutsPerWeek, hasOther, hasMachine,
                hasBarbell, hasDumbbell, hasKettlebell, hasCable, hasEasyCurlBar, hasNone, hasBands, hasMedicineBall,
                hasExerciseBall, hasFoamRoll, wantsBodyOnly));
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
        string gender,
        DateTime createdAt,

        bool isProUser,
        int workoutsPerWeek,
        bool hasOther,
        bool hasMachine,
        bool hasBarbell,
        bool hasDumbbell,
        bool hasKettlebell,
        bool hasCable,
        bool hasEasyCurlBar,
        bool hasNone,
        bool hasBands,
        bool hasMedicineBall,
        bool hasExerciseBall,
        bool hasFoamRoll,
        bool wantsBodyOnly)
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

        return Result.FirstFailureOrSuccess(dateOfBirthResult, weightResult, heightResult, goalResult, dailyStepsResult,
                hoursOfSleepResult, genderResult)
            .Map(() => new PersonalData(userId, dateOfBirth, weight, height, medicalHistory, currentIllnesses, goal,
                unwantedExercises, dailySteps, hoursOfSleep, gender, createdAt, isProUser, workoutsPerWeek, hasOther,
                hasMachine, hasBarbell, hasDumbbell, hasKettlebell, hasCable, hasEasyCurlBar, hasNone, hasBands,
                hasMedicineBall, hasExerciseBall, hasFoamRoll, wantsBodyOnly));
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

    public bool IsProUser { get; private set; }

    public int WorkoutsPerWeek { get; private set; }

    public bool HasOther { get; private set; }

    public bool HasMachine { get; private set; }

    public bool HasBarbell { get; private set; }

    public bool HasDumbbell { get; private set; }

    public bool HasKettlebells { get; private set; }

    public bool HasCable { get; private set; }

    public bool HasEasyCurlBar { get; private set; }

    public bool HasNone { get; private set; }

    public bool HasBands { get; private set; }

    public bool HasMedicineBall { get; private set; }

    public bool HasExerciseBall { get; private set; }

    public bool HasFoamRoll { get; private set; }

    public bool WantsBodyOnly { get; private set; }

    public DateTime CreatedAt { get; private set; }
}