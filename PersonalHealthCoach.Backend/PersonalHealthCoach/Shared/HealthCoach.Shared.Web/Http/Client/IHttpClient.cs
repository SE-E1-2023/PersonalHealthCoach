namespace HealthCoach.Shared.Web;

public interface IHttpClient
{
    public IHttpClient OnRoute(string route);

    public Task<TResult> Post<TRequest, TResult>(TRequest request) where TRequest : class where TResult : class;

    public Task<TResult> Get<TResult>() where TResult : class;
}