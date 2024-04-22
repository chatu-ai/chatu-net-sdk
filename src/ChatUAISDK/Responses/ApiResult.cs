using System;
using Newtonsoft.Json;

namespace ChatUAISDK.Responses;

public class ApiResult
{
    [JsonProperty(Order = 0)] public int Code { get; set; }

    [JsonProperty(Order = 99)] public string Message { get; set; }

    /// <summary>
    /// API execution time
    /// </summary>
    [JsonProperty(Order = 100)]
    public TimeSpan? Elapsed { get; set; }

    public static ApiResult<T> Ok<T>(T data)
    {
        return new ApiResult<T>
        {
            Data = data
        };
    }
}

public class ApiResult<T> : ApiResult
{
    [JsonProperty(Order = 10)] public T Data { get; set; }
}