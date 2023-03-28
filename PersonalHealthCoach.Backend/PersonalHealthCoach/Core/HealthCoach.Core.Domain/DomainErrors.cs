namespace HealthCoach.Core.Domain;

public static class DomainErrors
{
    public static class User
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(User)}.{nameof(Create)}";

            public const string NameNullOrEmpty = $"{Prefix}.{nameof(NameNullOrEmpty)}";
            public const string FirstNameNullOrEmpty = $"{Prefix}.{nameof(FirstNameNullOrEmpty)}";
            public const string EmailAddressNullOrEmpty = $"{Prefix}.{nameof(EmailAddressNullOrEmpty)}";
        }
    }
}