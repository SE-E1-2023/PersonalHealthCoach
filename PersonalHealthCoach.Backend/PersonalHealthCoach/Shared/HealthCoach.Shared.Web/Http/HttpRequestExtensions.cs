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
}
