using CSharpFunctionalExtensions;

namespace HealthCoach.Shared.Core;

public static class StringExtensions
{
    public static Result<T> EnsureNotNull<T>(this T subject, string error)
    {
        return Result.FailureIf(subject == null, subject, error);
    }

    public static Result<string> EnsureNotNullOrEmpty(this string subject, string error)
    {
        return Result.SuccessIf(!string.IsNullOrEmpty(subject?.Trim()), subject, error);
    }
}