using System.Net.Http.Json;

namespace HealthCoach.Shared.Web;

public class HealthCoachHttpClient : IHttpClient
{
    private readonly HttpClient httpClient;
    
    private HealthCoachHttpClient() { }

    public HealthCoachHttpClient(string baseUrl)
    {
        BaseUrl = baseUrl;

        if (!BaseUrl.EndsWith('/'))
        {
            BaseUrl += '/';
        }

        httpClient = new()
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public IHttpClient OnRoute(string route)
    {
        this.Route = route;

        return this;
    }

    public async Task<TResult> Post<TRequest, TResult>(TRequest request) where TRequest : class where TResult : class
    {
        var response = await httpClient.PostAsJsonAsync(Route, request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        return await response.Content.ReadFromJsonAsync<TResult>();
    }

    public async Task<TResult> Get<TResult>() where TResult : class
    {
        var response = await httpClient.GetAsync(Route);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        return await response.Content.ReadFromJsonAsync<TResult>();
    }

    public string BaseUrl { get; private init; }

    public string Route { get; private set; }
}