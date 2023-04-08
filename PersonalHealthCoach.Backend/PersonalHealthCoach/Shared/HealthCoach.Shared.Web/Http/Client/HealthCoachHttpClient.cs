using System.Net.Http.Json;
using System.Text.Json;
using CSharpFunctionalExtensions;

namespace HealthCoach.Shared.Web;

public class HealthCoachHttpClient : IHttpClient
{
    private readonly HttpClient httpClient;
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

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

    public async Task<Result<TResult>> Post<TRequest, TResult>(TRequest request) where TRequest : class where TResult : class
    {
        var response = await httpClient.PostAsJsonAsync(Route, request, jsonSerializerOptions);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<TResult>($"Request failed with status code {response.StatusCode}");
        }

        return Result.Success(await response.Content.ReadFromJsonAsync<TResult>());
    }

    public async Task<Result<TResult>> Get<TResult>() where TResult : class
    {
        var response = await httpClient.GetAsync(Route);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<TResult>($"Request failed with status code {response.StatusCode}");
        }

        return Result.Success(await response.Content.ReadFromJsonAsync<TResult>());
    }

    public string BaseUrl { get; private init; }

    public string Route { get; private set; }
}