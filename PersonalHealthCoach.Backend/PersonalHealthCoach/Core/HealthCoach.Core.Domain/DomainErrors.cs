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

    public static class PersonalData
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(PersonalData)}.{nameof(Create)}";

            public const string DateOfBirthNull = $"{Prefix}.{nameof(Domain.PersonalData.DateOfBirth)}.{nameof(DateOfBirthNull)}";
            public const string UserNotOldEnough = $"{Prefix}.{nameof(Domain.PersonalData.DateOfBirth)}.{nameof(UserNotOldEnough)}";
            
            public const string InvalidWeight = $"{Prefix}.{nameof(Domain.PersonalData.Weight)}.{nameof(InvalidWeight)}";
            public const string InvalidHeight = $"{Prefix}.{nameof(Domain.PersonalData.Height)}.{nameof(InvalidHeight)}";
            public const string InvalidDailySteps = $"{Prefix}.{nameof(Domain.PersonalData.DailySteps)}.{nameof(InvalidDailySteps)}";
            public const string InvalidHoursOfSleep = $"{Prefix}.{nameof(Domain.PersonalData.HoursOfSleep)}.{nameof(InvalidHoursOfSleep)}";
            public const string InvalidGender = $"{Prefix}.{nameof(Domain.PersonalData.Gender)}.{nameof(InvalidGender)}";

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

    public static class PersonalTip
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(PersonalTip)}.{nameof(Create)}";

            public const string TipTypeNullOrEmpty = $"{Prefix}.{nameof(TipTypeNullOrEmpty)}";
            public const string TipNullOrEmpty = $"{Prefix}.{nameof(TipNullOrEmpty)}";
        }
    }

    public static class Report
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(Report)}.{nameof(Create)}";

            public const string TargetNullOrEmpty = $"{Prefix}.{nameof(TargetNullOrEmpty)}";
            public const string InvalidTarget = $"{Prefix}.{nameof(InvalidTarget)}";
            public const string ReasonNullOrEmpty = $"{Prefix}.{nameof(ReasonNullOrEmpty)}";
        }

        public static class Solve
        {
            private const string Prefix = $"{nameof(Report)}.{nameof(Solve)}";

            public const string AlreadySolved = $"{Prefix}.{nameof(AlreadySolved)}";
        }
    }

    public static class WellnessTip
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(PersonalTip)}.{nameof(Create)}";

            public const string TipNullOrEmpty = $"{Prefix}.{nameof(TipNullOrEmpty)}";
        }
    }
}