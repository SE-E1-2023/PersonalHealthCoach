namespace HealthCoach.Shared.Web;

public class HttpClientFactory : IHttpClientFactory
{
    public IHttpClient OnBaseUrl(string route)
    {
        return new HealthCoachHttpClient(route);
    }
}