using System.Net;
using CSharpFunctionalExtensions;
using Microsoft.Azure.Functions.Worker.Http;

namespace HealthCoach.Shared.Web;

public static class ResultExtensions
{
    public static async Task<HttpResponseData> ToResponseData<T>(this Task<Result<T>> resultTask, HttpRequestData request, Action<HttpResponseData, ApiResult> onOk)
    {
        var response = request.CreateResponse();

        var apiResult = ApiResult.From(await resultTask);
        if (apiResult.IsSuccess)
        {
            onOk(response, apiResult);
        }
        else
        {
            await response.WriteAsJsonAsync(apiResult, HttpStatusCode.BadRequest);
        }

        return response;
    }

    public static async Task<HttpResponseData> ToResponseData(this Result result, HttpRequestData request, Action<HttpResponseData, ApiResult> onOk = null)
    {
        var response = request.CreateResponse(HttpStatusCode.NoContent);

        var apiResult = ApiResult.From(result);
        if (apiResult.IsSuccess)
        {
            onOk?.Invoke(response, apiResult);
        }
        else
        {
            await response.WriteAsJsonAsync(apiResult, HttpStatusCode.BadRequest);
        }

        return response;
    }

    public static async Task<HttpResponseData> ToResponseData(this Task<Result> resultTask, HttpRequestData request, Action<HttpResponseData, ApiResult> onOk = null)
        => await (await resultTask).ToResponseData(request, onOk);
}