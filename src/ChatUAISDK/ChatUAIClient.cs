using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatUAISDK.Requests;
using ChatUAISDK.Responses;
using Newtonsoft.Json;

namespace ChatUAISDK;

public class ChatUAIClient
{
    private readonly string _accessToken;
    private readonly string _baseUrl;

    public ChatUAIClient(string baseUrl, string accessToken)
    {
        _baseUrl = baseUrl;
        _accessToken = accessToken;
    }

    /// <summary>
    /// Normal Q&amp;A request, due to the request is easy to timeout, please set a timeout time more than 5 minutes
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<AskResponse>> AskAsync(AskRequest request)
    {
        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(10);
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            prompt = request.Prompt,
            conversationId = request.ConversationId,
            sceneId = (int) (request.SceneId ?? 0),
            system = request.System,
            model = request.Model,
            maxTokens = request.MaxTokens,
            textVerficationLevel = request.TextVerificationLevel,
            temprature = request.Temperature,
            assistantId = request.AssistantId
        });
        var response = await client.PostAsync($"{_baseUrl}/chat/ask",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        Console.WriteLine(text);
        return JsonConvert.DeserializeObject<ApiResult<AskResponse>>(text);
    }

    /// <summary>
    ///     Create a stream input request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<StreamResponse>> StreamCreateAsync(StreamCreateRequest request)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            prompt = request.Prompt,
            sceneId = (int) (request.SceneId ?? 0),
            system = request.System,
            conversationId = request.ConversationId,
            useEscape = request.UseEscape,
            model = request.Model,
            maxTokens = request.MaxTokens,
            textVerificationLevel = request.TextVerificationLevel,
            temperature = request.Temperature,
            assistantId = request.AssistantId
        });
        var response = await client.PostAsync($"{_baseUrl}/chat/stream/create",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<StreamResponse>>(text);
    }

    /// <summary>
    ///     Get stream output, if you are using front-end, please use EventSource
    /// </summary>
    /// <param name="streamId"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async IAsyncEnumerable<string> StreamAsync(Guid streamId)
    {
        using var client = new HttpClient();
        var url = $"{_baseUrl}/chat/stream?streamId={streamId}";
        using var request = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
        {
            var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException(
                $"StreamAsync Failed! HTTP status code: {response.StatusCode} | Response body: {responseAsString}",
                null, response.StatusCode);
        }

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(contentStream);
        while (await reader.ReadLineAsync().ConfigureAwait(false) is { } line)
        {
            if (line.StartsWith("event: "))
            {
                var evt = line["event: ".Length..].Trim();
                if (evt == "close") break;
            }

            //Console.WriteLine(line);
            if (line.StartsWith("data: "))
            {
                line = line["data: ".Length..]
                    .Replace("\r", "\n")
                    .Replace("<c-api-line>", "\n");
                yield return line;
            }

            if (!string.IsNullOrWhiteSpace(line)) continue;
        }
    }


    /// <summary>
    /// Return the consumed token, this token is the ChatU token after conversion
    /// </summary>
    /// <param name="streamId"></param>
    /// <returns></returns>
    public async Task<ApiResult<SyncResponse>> SyncAsync(Guid streamId)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            streamId
        });
        var response = await client.PostAsync($"{_baseUrl}/chat/stream/sync",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<SyncResponse>>(text);
    }

    /// <summary>
    /// Generate image API
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<CreateImageResponse>> CreateImageAsync(CreateImageRequest request)
    {
        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(15);
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            prompt = request.Prompt,
            style = request.Style,
            count = request.Count ?? 1,
            promptOptimize = request.PromptOptimize
        });
        var response = await client.PostAsync($"{_baseUrl}/draw/createImage",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<CreateImageResponse>>(text);
    }

    /// <summary>
    ///     Get image URL
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<CheckResultResponse>> CheckResultAsync(CheckResultRequest request)
    {
        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(15);
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            requestId = request.RequestId
        });
        var response = await client.PostAsync($"{_baseUrl}/draw/checkResult",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<CheckResultResponse>>(text);
    }

    /// <summary>
    ///     Get the information of the current AccessToken
    /// </summary>
    public async Task<ApiResult<TokenInfoResponse>> TokenInfoAsync()
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken
        });
        var response = await client.PostAsync($"{_baseUrl}/account/key/info",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<TokenInfoResponse>>(text);
    }
}