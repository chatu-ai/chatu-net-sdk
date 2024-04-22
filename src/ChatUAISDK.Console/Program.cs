using System.Diagnostics;
using ChatUAISDK;
using ChatUAISDK.Requests;
var testApiUrl = "https://api.chatuapi.com";
//  AccessToken is available at https://admin.chatu.pro
string? accessToken;
if (args.Length == 0)
{
    Console.WriteLine("Please input AccessToken");
    accessToken = Console.ReadLine();
}
else
{
    accessToken = args[0]; // "your access token";
}

var client = new ChatUAIClient(testApiUrl, accessToken);

var stopwatch = new Stopwatch();
while (true)
{
    Console.WriteLine("Which mode to use\n1, Normal mode\n2, Stream mode");
    var mode = Console.ReadLine();
    Console.WriteLine("Whether to use conversation[Y/N]");
    var isConversationId = Console.ReadLine();
    Guid? conversationId = null;
    if (isConversationId == "Y")
    {
        conversationId = Guid.NewGuid();
    }

    while (true)
    {

        Console.Write("You：\t");
        var prompt = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(prompt))
        {
            break;
        }
        if (mode == "1")
        {
            stopwatch.Start();
            var askResponse = await client.AskAsync(new AskRequest
            {
                Prompt = prompt,
                ConversationId = conversationId,
            });
            if (askResponse.Code == 0)
            {
                Console.WriteLine($"AI:\t{askResponse.Data.Answer}");
                Console.WriteLine($"\t\tConversationId:{askResponse.Data.ConversationId}");
                Console.WriteLine($"\t\tToken:{askResponse.Data.Token}");
            }
            else
            {
                Console.WriteLine($"Error:{askResponse.Message}");
            }

            stopwatch.Stop();
            Console.WriteLine($"\t\tTime:{stopwatch.ElapsedMilliseconds}ms");
        }
        else if (mode == "2")
        {
            stopwatch.Start();
            var streamCreateResponse = await client.StreamCreateAsync(
                new StreamCreateRequest()
            {
                Prompt = prompt,
                ConversationId = conversationId,
                System = "Reply in standard Markdown and support Latex, Mermaid format",
                UseEscape = true
            });
            if (streamCreateResponse.Code == 0)
            {
                await foreach (var item in client.StreamAsync(streamCreateResponse.Data.StreamId))
                {
                    Console.Write(item);
                }
            }
            else
            {
                Console.WriteLine($"Error:{streamCreateResponse.Message}");
            }
            stopwatch.Stop();
            Console.WriteLine($"\t\tTime：{stopwatch.ElapsedMilliseconds}ms");
            var syncResult = await client.SyncAsync(streamCreateResponse.Data.StreamId);
            Console.WriteLine($"Cost {syncResult.Data.Token},RequestId:{syncResult.Data.RequestId}");
        }
    }
}