namespace HealthCoach.Core.Domain;

public static class DomainErrors
{
    public static class User
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(User)}.{nameof(Create)}";

            public const string NameNullOrEmpty = $"{Prefix}.{nameof(Domain.User.Name)}.{nameof(NameNullOrEmpty)}";
            public const string FirstNameNullOrEmpty = $"{Prefix}.{nameof(Domain.User.FirstName)}.{nameof(FirstNameNullOrEmpty)}";
            public const string EmailAddressNullOrEmpty = $"{Prefix}.{nameof(Domain.User.EmailAddress)}.{nameof(EmailAddressNullOrEmpty)}";
            public const string InvalidEmailAddressFormat = $"{Prefix}.{nameof(Domain.User.EmailAddress)}.{nameof(InvalidEmailAddressFormat)}";
        }
    }

    public static class PersonalData
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(PersonalData)}.{nameof(Create)}";

            public const string DateOfBirthNull = $"{Prefix}.{nameof(Domain.PersonalData.DateOfBirth)}.{nameof(DateOfBirthNull)}";
            public const string UserNotOldEnough = $"{Prefix}.{nameof(Domain.PersonalData.DateOfBirth)}.{nameof(UserNotOldEnough)}";
            
            public const string InvalidWeight = $"{Prefix}.{nameof(Domain.PersonalData.Weight)}.{nameof(InvalidWeight)}";

            public const string InvalidHeight = $"{Prefix}.{nameof(Domain.PersonalData.Height)}.{nameof(InvalidHeight)}";

            public const string GoalIsNullOrEmpty = $"{Prefix}.{nameof(Domain.PersonalData.Goal)}.{nameof(GoalIsNullOrEmpty)}";
            public const string GoalIsUnrecognized = $"{Prefix}.{nameof(Domain.PersonalData.Goal)}.{nameof(GoalIsUnrecognized)}";
        }
    }

    public static class FitnessPlan
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(FitnessPlan)}.{nameof(Create)}";

            public const string NoExercises = $"{Prefix}.{nameof(NoExercises)}";
        }
    }
}