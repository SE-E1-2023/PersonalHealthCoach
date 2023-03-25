using System.Net;
using Newtonsoft.Json;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Shared.Web;

public static class HttpRequestExtensions
{
    public static async Task<Result<T>> DeserializeBodyPayload<T>(this HttpRequestData request) where T : class
    {
        using var reader = new StreamReader(request.Body);
        var body = JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());

        return Result.SuccessIf(body != null, body, "BAD_REQUEST");
    }

    public static async Task WriteBadRequestResponse(this HttpRequestData httpRequest, FunctionContext context, string error)
    {
        var response = httpRequest.CreateResponse(HttpStatusCode.BadRequest);
        await response.WriteStringAsync(error);

        var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
        var functionBindingsFeature = keyValuePair.Value;
        var type = functionBindingsFeature.GetType();
        var result = type.GetProperties().Single(p => p.Name == "InvocationResult");
        result.SetValue(functionBindingsFeature, response);
    }

    public static async Task<HttpResponseData> CreateJsonResponse<T>(this HttpRequestData httpRequest, T payload)
    {
        var resp = httpRequest.CreateResponse(HttpStatusCode.OK);
        await resp.WriteAsJsonAsync(payload);

        return resp;
    }
}

public static class StringExtensions
{
    public static Result<T> Deserialize<T>(this string json) where T : class
    {
        return Result.FailureIf(string.IsNullOrEmpty(json), "Empty json")
            .Map(() => JsonConvert.DeserializeObject<T>(json)!)
            .Ensure(obj => obj != null, $"Json does not respect {typeof(T).Name} contract");
    }
}