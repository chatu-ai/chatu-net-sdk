using System;

namespace ChatUAISDK.Requests
{
    public class AskRequest
    {
        public string Prompt { get; set; }
        public Guid? ConversationId { get; set; }
    }
}