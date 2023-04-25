using System;

namespace ChatUAISDK.Responses;

public class SyncResponse
{
    public long? Token { get; set; }
    public Guid StreamId { get; set; }
    public string? RequestId { get; set; }
}