using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain
{
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
            var typeResult = type.EnsureNotNullOrEmpty(DomainErrors.PersonalTip.Create.NoTipType);
            var tipResult = tipText.EnsureNotNullOrEmpty(DomainErrors.PersonalTip.Create.NoTip);

            return Result.FirstFailureOrSuccess(typeResult, tipResult)
            .Map(() => new PersonalTip(userId,typeResult.Value, tipResult.Value));
        }

        public Guid UserId { get; set; }
        public string? Type { get; set; }
        public string? TipText { get; set; }


    }
}
