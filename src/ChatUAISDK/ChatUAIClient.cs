using System;
using System.Collections.Generic;
using ChatUAISDK.Requests;
using ChatUAISDK.Responses;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<ApiResult<AskResponse>> AskAsync(AskRequest request)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            prompt = request.Prompt,
            conversationId = request.ConversationId
        });
        var response =await client.PostAsync($"{_baseUrl}/chat/ask",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<AskResponse>>(text);
    }
    public async Task<ApiResult<StreamResponse>> StreamCreateAsync(AskRequest request)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(new
        {
            accessToken = _accessToken,
            prompt = request.Prompt,
            conversationId = request.ConversationId
        });
        var response = await client.PostAsync($"{_baseUrl}/chat/stream/create",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var text = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResult<StreamResponse>>(text);
    }
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
        using var reader = new System.IO.StreamReader(contentStream);
        while (await reader.ReadLineAsync().ConfigureAwait(false) is { } line )
        {
            if (line.StartsWith("event: "))
            {
                var evt = line["event: ".Length..].Trim();
                if (evt == "close")
                {
                    break;
                }
            }
            //Console.WriteLine(line);
            if (line.StartsWith("data: "))
            {
                line = line["data: ".Length..].Replace("\r", "\n");
                yield return line;
            }

            if (!string.IsNullOrWhiteSpace(line))
            {
               continue;
            }
        }
    }
}