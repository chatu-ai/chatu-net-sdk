using System;

namespace ChatUAISDK.Requests;

public class StreamCreateRequest
{
    /// <summary>
    /// 提示词
    /// </summary>
    public string Prompt { get; set; }
    /// <summary>
    /// 会话 Id
    /// </summary>
    public Guid? ConversationId { get; set; }
    /// <summary>
    /// 用于切换GPT-3.5或GPT-4
    /// </summary>
    public ChatSceneType? SceneId { get; set; }
    /// <summary>
    /// 如设置为true 则会将 \n 以 &lt;c-api-line&gt;返回，此选项仅stream/create生效
    /// </summary>
    public bool UseEscape { get; set; }
    /// <summary>
    /// 系统设定
    /// </summary>
    public string System { get; set; }
}