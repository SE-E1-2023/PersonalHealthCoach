namespace HealthCoach.Shared.Web;

public interface IHttpClientFactory
{
    public IHttpClient OnBaseUrl(string route);
}