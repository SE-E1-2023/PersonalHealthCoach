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
    }
}