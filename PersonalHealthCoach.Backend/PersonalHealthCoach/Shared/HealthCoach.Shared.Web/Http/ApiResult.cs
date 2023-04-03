using CSharpFunctionalExtensions;

namespace HealthCoach.Shared.Web;

public sealed class ApiResult
{
    private ApiResult(bool isFailure, bool isSuccess, string errorCode, object value = null)
    {
        IsFailure = isFailure;
        IsSuccess = isSuccess;
        ErrorCode = errorCode;
        Value = value;
    }

    internal static ApiResult From(Result result) => new(result.IsFailure, result.IsSuccess, result.IsFailure ? result.Error : string.Empty);

    internal static ApiResult From<T>(Result<T> result) => new(result.IsFailure, result.IsSuccess, result.IsFailure ? result.Error : string.Empty, result.IsSuccess ? (object)result.Value : null);

    public bool IsFailure { get; private set; }

    public bool IsSuccess { get; private set; }

    public string ErrorCode { get; private set; }

    public object Value { get; private set; }
}