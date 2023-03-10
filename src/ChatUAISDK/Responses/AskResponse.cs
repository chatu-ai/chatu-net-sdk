using System;

namespace ChatUAISDK.Responses
{
    public class AskResponse
    {
        public string Answer { get; set; }
        public Guid? ConversationId { get; set; }
        public long Token { get; set; }
    }
}