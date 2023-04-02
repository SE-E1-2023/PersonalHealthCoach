namespace HealthCoach.Shared.Web;

public class HttpClientFactory : IHttpClientFactory
{
    public HttpClientFactory() { }

    public IHttpClient OnBaseUrl(string route)
    {
        return new HealthCoachHttpClient(route);
    }
}