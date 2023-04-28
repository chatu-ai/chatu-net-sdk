using System.Diagnostics;
using ChatUAISDK;
using ChatUAISDK.Requests;
var testApiUrl = "https://api.chatuapi.com";
// AccessToken 请于 https://agent.chatu.ai 获取
var accessToken = "";
var client = new ChatUAIClient(testApiUrl, accessToken);

var stopwatch = new Stopwatch();
while (true)
{
    Console.WriteLine("使用哪种模式\n1，普通模式\n2，流试访问");
    var mode = Console.ReadLine();
    Console.WriteLine("是否使用会话[Y/N]");
    var isConversationId = Console.ReadLine();
    Guid? conversationId = null;
    if (isConversationId == "Y")
    {
        conversationId = Guid.NewGuid();
    }

    while (true)
    {

        Console.Write("您：\t");
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
            Console.WriteLine($"\t\t耗时：{stopwatch.ElapsedMilliseconds}ms");
        }
        else if (mode == "2")
        {
            stopwatch.Start();
            var streamCreateResponse = await client.StreamCreateAsync(
                new StreamCreateRequest()
            {
                Prompt = prompt,
                ConversationId = conversationId,
                System = "使用标准Markdown回复，并支持Latex，Mermaid格式",
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
            Console.WriteLine($"\t\t耗时：{stopwatch.ElapsedMilliseconds}ms");
            var syncResult = await client.SyncAsync(streamCreateResponse.Data.StreamId);
            Console.WriteLine($"消耗{syncResult.Data.Token},RequestId:{syncResult.Data.RequestId}");
        }


    }
}