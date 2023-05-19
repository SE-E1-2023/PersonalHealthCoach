namespace HealthCoach.Presentation.Tests;

public sealed record PersonalDataMock(Guid UserId,
    DateTime? DateOfBirth,
    float Weight,
    float Height,
    List<string> MedicalHistory,
    List<string> CurrentIllnesses,
    string Goal,
    List<string> UnwantedExercises,
    int? DailySteps,
    double? HoursOfSleep,
    string Gender);
