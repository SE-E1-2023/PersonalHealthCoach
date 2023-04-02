using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;

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
        List<string> unwantedExercises)
    {
        UserId = userId;
        DateOfBirth = (DateTime)dateOfBirth;
        Weight = weight;
        Height = height;
        MedicalHistory = medicalHistory ?? new List<string>();
        CurrentIllnesses = currentIllnesses ?? new List<string>();
        Goal = goal;
        UnwantedExercises = unwantedExercises ?? new List<string>();
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
        List<string> unwantedExercises)
    {
        var dateOfBirthResult = dateOfBirth
            .EnsureNotNull(Errors.DateOfBirthNull)
            .Ensure(d => d <= PersonalDataConstants.MinimumDateOfBirth, Errors.UserNotOldEnough);

        var weightResult = Result.SuccessIf(weight > 0, Errors.InvalidWeight);

        var heightResult = Result.SuccessIf(height > 0, Errors.InvalidHeight);

        var goalResult = goal
            .EnsureNotNullOrEmpty(Errors.GoalIsNullOrEmpty)
            .Ensure(g => PersonalDataConstants.AllowedGoals.Contains(g), Errors.GoalIsUnrecognized);

        return Result.FirstFailureOrSuccess(dateOfBirthResult, weightResult, heightResult, goalResult)
            .Map(() => new PersonalData(userId, dateOfBirth, weight, height, medicalHistory, currentIllnesses, goal, unwantedExercises));
    }

    public Guid UserId { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public float Weight { get; private set; }

    public float Height { get; private set; }

    public List<string> MedicalHistory { get; private set; }

    public List<string> CurrentIllnesses { get; private set; }

    public string Goal { get; private set; }

    public List<string> UnwantedExercises { get; private set; }

    public DateTime CreatedAt { get; private set; }
}

