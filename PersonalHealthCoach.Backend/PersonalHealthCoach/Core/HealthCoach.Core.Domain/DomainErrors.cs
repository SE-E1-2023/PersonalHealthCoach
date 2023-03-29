﻿namespace HealthCoach.Core.Domain;

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
}