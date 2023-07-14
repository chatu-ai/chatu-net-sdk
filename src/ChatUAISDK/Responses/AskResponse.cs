using System;

namespace ChatUAISDK.Responses
{
    public class AskResponse
    {
        /// <summary>
        /// 问题
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 会话ID 如果之前未传递，则会新生成一个
        /// </summary>
        public Guid? ConversationId { get; set; }
        /// <summary>
        /// 当前问答消耗的Token
        /// </summary>
        public long Token { get; set; }
        /// <summary>
        /// 返回当前调用所使用的模型
        /// </summary>
        public string Model { get; set; }
    }
}