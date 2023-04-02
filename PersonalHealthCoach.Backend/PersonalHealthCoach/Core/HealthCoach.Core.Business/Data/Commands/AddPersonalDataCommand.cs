using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public record AddPersonalDataCommand(
    Guid UserId,
    DateTime? DateOfBirth,
    float Weight, 
    float Height, 
    List<string> MedicalHistory,
    List<string> CurrentIllnesses,
    string Goal, 
    List<string> UnwantedExercises) : IRequest<Result<PersonalData>>;
