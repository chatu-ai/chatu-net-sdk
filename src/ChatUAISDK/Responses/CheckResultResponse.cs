
namespace ChatUAISDK.Responses;

public class CheckResultResponse
{
    public int Status { get; set; }


    public List<Keys> Keys { get; set; } = null!;
}

public class Keys
{
    public string Key { get; set; } = null!;
}