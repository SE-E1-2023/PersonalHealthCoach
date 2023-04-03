namespace HealthCoach.Core.Business;

public static class BusinessErrors
{
    public static class User
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(User)}.{nameof(Create)}";

            public const string EmailAddressAlreadyInUse = $"{Prefix}.{nameof(EmailAddressAlreadyInUse)}";
        }

        public static class Get
        {
            private const string Prefix = $"{nameof(User)}.{nameof(Get)}";

            public const string EmailAddressDoesntExist = $"{Prefix}.{nameof(EmailAddressDoesntExist)}";
        }
    }

    public static class PersonalData
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(PersonalData)}.{nameof(Create)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
        }

        public static class Get
        {
            private const string Prefix = $"{nameof(PersonalData)}.{nameof(Get)}";

            public const string UserIdNotFound = $"{Prefix}.{nameof(UserIdNotFound)}";
        }
    }
}