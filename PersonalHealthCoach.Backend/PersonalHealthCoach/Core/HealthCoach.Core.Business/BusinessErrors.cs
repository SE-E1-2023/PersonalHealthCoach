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

        public static class Delete
        {
            private const string Prefix = $"{nameof(FitnessPlan)}.{nameof(Delete)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
            public const string FitnessPlanNotFound = $"{Prefix}.{nameof(FitnessPlanNotFound)}";
            public const string UserNotAuthorized = $"{Prefix}.{nameof(UserNotAuthorized)}";
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

        public static class Get
        {
            private const string Prefix = $"{nameof(PersonalTip)}.{nameof(Get)}";

            public const string PersonalTipNotFound = $"{Prefix}.{nameof(PersonalTipNotFound)}";

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
        public static class Create
        {
            private const string Prefix = $"{nameof(DietPlan)}.{nameof(Create)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
            public const string PersonalDataNotFound = $"{Prefix}.{nameof(PersonalDataNotFound)}";
        }
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
            public const string UserNotAuthorized = $"{Prefix}.{nameof(UserNotAuthorized)}";
            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
        }
    }

    public static class Report
    {
        public static class Solve
        {
            private const string Prefix = $"{nameof(Report)}.{nameof(Solve)}";

            public const string ReportNotFound = $"{Prefix}.{nameof(ReportNotFound)}";
            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
            public const string InvalidReportType = $"{Prefix}.{nameof(InvalidReportType)}";
            public const string ReportAlreadySolved = $"{Prefix}.{nameof(ReportAlreadySolved)}";
            public const string UserNotAuthorized = $"{Prefix}.{nameof(UserNotAuthorized)}";
        }
    }

    public static class ExerciseHistory
    {
        public static class AddExercises
        {
            private const string Prefix = $"{nameof(ExerciseHistory)}.{nameof(AddExercises)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
        }

        public static class GetExercices
        {
            private const string Prefix = $"{nameof(ExerciseHistory)}.{nameof(GetExercices)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";

            public const string LogIsEmpty = $"{Prefix}.{nameof(LogIsEmpty)}";
        }
    }
    public static class FoodHistory
    {
        public static class AddFoods
        {
            private const string Prefix = $"{nameof(FoodHistory)}.{nameof(AddFoods)}";
            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";

        }

        public static class Get
        {
            private const string Prefix = $"{nameof(FoodHistory)}.{nameof(Get)}";
            public const string FoodHistoryNotFound = $"{Prefix}.{nameof(FoodHistoryNotFound)}";
        }
    }

    public static class WellnessPlan
    {
        public static class Create
        {
            private const string Prefix = $"{nameof(WellnessPlan)}.{nameof(Create)}";

            public const string UserNotFound = $"{Prefix}.{nameof(UserNotFound)}";
        }
    }
}