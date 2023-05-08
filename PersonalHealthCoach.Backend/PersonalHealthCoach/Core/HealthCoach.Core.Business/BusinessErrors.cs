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

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
            public const string PersonalDataNotFound = $"{Prefix}.{nameof(PersonalDataNotFound)}";
        }
    }

    public static class FitnessPlan
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(FitnessPlan)}.{nameof(Create)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
            public const string PersonalDataNotFound = $"{Prefix}.{nameof(PersonalDataNotFound)}";
        }

        public static class Get
        {
            private const string Prefix = $"{nameof(FitnessPlan)}.{nameof(Get)}";

            public const string FitnessPlanNotFound = $"{Prefix}.{nameof(FitnessPlanNotFound)}";

        }

        public static class Report
        {
            private const string Prefix = $"{nameof(FitnessPlan)}.{nameof(Report)}";

            public const string ReportAlreadyExists = $"{Prefix}.{nameof(ReportAlreadyExists)}";
            public const string FitnessPlanDoesNotExist = $"{Prefix}.{nameof(FitnessPlanDoesNotExist)}";
        }
    }

    public static class PersonalTip
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(PersonalTip)}.{nameof(Create)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
            public const string PersonalDataNotFound = $"{Prefix}.{nameof(PersonalDataNotFound)}";
        }

        public static class Report
        {
            private const string Prefix = $"{nameof(PersonalTip)}.{nameof(Report)}";

            public const string ReportAlreadyExists = $"{Prefix}.{nameof(ReportAlreadyExists)}";
            public const string PersonalTipDoesNotExist = $"{Prefix}.{nameof(PersonalTipDoesNotExist)}";
        }
    }

    public static class WellnessTip
    {
        public static class GetRandomTip
        {
            private const string Prefix = $"{nameof(WellnessTip)}.{nameof(GetRandomTip)}";

            public const string TipDoesNotExist = $"{Prefix}.{nameof(TipDoesNotExist)}";
        }
    }

    public static class DietPlan
    {
        public static class Report
        {
            private const string Prefix = $"{nameof(DietPlan)}.{nameof(Report)}";

            public const string ReportAlreadyExists = $"{Prefix}.{nameof(ReportAlreadyExists)}";
            public const string DietPlanDoesNotExist = $"{Prefix}.{nameof(DietPlanDoesNotExist)}";
        }

        public static class Delete
        {
            private const string Prefix = $"{nameof(DietPlan)}.{nameof(Delete)}";

            public const string DietPlanDoesNotExist = $"{Prefix}.{nameof(DietPlanDoesNotExist)}";
        }
    }

    public static class ExerciseLog
    {
        public static class AddExercises
        {
            private const string Prefix = $"{nameof(ExerciseLog)}.{nameof(AddExercises)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
        }

        public static class GetExercices
        {
            private const string Prefix = $"{nameof(ExerciseLog)}.{nameof(GetExercices)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";

            public const string LogIsEmpty = $"{Prefix}.{nameof(LogIsEmpty)}";
        }
    }
}