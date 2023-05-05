using CSharpFunctionalExtensions;

namespace HealthCoach.Shared.Web;

public interface IHttpClient
{
    public IHttpClient OnRoute(string route);

    public Task<Result<TResult>> Post<TRequest, TResult>(TRequest request) where TRequest : class where TResult : class;

    public Task<Result<TResult>> Get<TResult>() where TResult : class;

    public Task<Result<TResult>> Patch<TRequest, TResult>(TRequest request) where TRequest : class where TResult : class;

    public Task<Result> Patch<TRequest>(TRequest request) where TRequest : class;

    public Task<Result> Delete<TRequest>(TRequest request) where TRequest : class;
}