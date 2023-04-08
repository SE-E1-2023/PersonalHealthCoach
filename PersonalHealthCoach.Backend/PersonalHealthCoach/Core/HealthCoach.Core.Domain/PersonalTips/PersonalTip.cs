using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain;

public class PersonalTip : AggregateRoot
{
    public PersonalTip()
    {
            
    }

    public PersonalTip(Guid userId, string type, string tipText)
    {
        UserId = userId;
        Type = type;
        TipText = tipText;
    }

    public static Result<PersonalTip> Create(Guid userId, string type, string tipText)
    {
        var typeResult = type.EnsureNotNullOrEmpty(DomainErrors.PersonalTip.Create.TipTypeNullOrEmpty);
        var tipResult = tipText.EnsureNotNullOrEmpty(DomainErrors.PersonalTip.Create.TipNullOrEmpty);

        return Result.FirstFailureOrSuccess(typeResult, tipResult)
        .Map(() => new PersonalTip(userId,type, tipText));
    }

    public Guid UserId { get; private set; }

    public string? Type { get; private set; }

    public string? TipText { get; private set; }
}

