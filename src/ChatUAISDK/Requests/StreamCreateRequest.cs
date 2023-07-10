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
    /// 模型(支持参数:gpt-3.5 gpt4.0 gpt-3.5-16k)
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// 用于切换GPT-3.5或GPT-4
    /// </summary>
    [Obsolete("SceneId is obsolete. Use Model  instead.", false)]
    public ChatSceneType? SceneId { get; set; }
    /// <summary>
    /// 系统设定
    /// </summary>
    public string System { get; set; }
    /// <summary>
    /// 如设置为true 则会将 \n 以 &lt;c-api-line&gt;返回，此选项仅stream/create生效
    /// </summary>
    public bool UseEscape { get; set; }
    /// <summary>
    /// 生成内容Token上限
    /// >0的整数，限制生成文字的Token
    /// 此Token为3.5或4的原生Token，而非计费Token
    /// 3.5 最多支持4096，4最多8192
    /// </summary>

    public int? MaxTokens { get; set; }
    /// <summary>
    /// 文本验证级别
    /// 取值 ：0，1，2
    /// 0 或 不传：不验证传入文本
    ///1：宽松文本验证
    ///2：严格文本验证
    /// </summary>
    public int? TextVerificationLevel { get; set; }
    /// <summary>
    /// 控制temperature参数
    ///参数范围(0.00,2.00)
    ///默认值0.9
    /// </summary>
    public float Temperature { get; set; }
    /// <summary>
    /// 使用的AI助手ID
    /// </summary>
    public int? AssistantId { get; set; }

}