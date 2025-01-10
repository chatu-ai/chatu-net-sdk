namespace ChatUAISDK.Requests;

public class AskRequest
{
    /// <summary>
    ///     prompt
    /// </summary>
    public string Prompt { get; set; } = null!;

    /// <summary>
    ///     conversation Id
    /// </summary>
    public Guid? ConversationId { get; set; }

    /// <summary>
    ///     model (eg: gpt-3.5 gpt4.0)
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    ///     Obsolete : Used to switch between GPT-3.5 or GPT-4.
    ///     use Model instead.
    /// </summary>
    [Obsolete("SceneId is obsolete. Use Model  instead.", true)]
    public ChatSceneType? SceneId { get; set; }

    /// <summary>
    ///     system prompt
    /// </summary>
    public string? System { get; set; }

    /// <summary>
    ///     en: The maximum number of tokens generated
    ///     > 0 integer, limit the number of tokens generated
    ///     this token is the native token of model, not the credits
    ///     eg : 4.0 supports up to 8192
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    ///     Text verification level
    ///     values: 0, 1, 2
    ///     0 or not passed: no text verification
    ///     1 : loose text verification
    ///     2 : strict text verification
    ///     9 : super strict text verification
    /// </summary>
    public int? TextVerificationLevel { get; set; }

    /// <summary>
    ///     Controls randomness in boltzmann sampling.
    ///     Lower temperature results in less randomness.
    ///     As the temperature approaches zero, the model will become deterministic and repetitive.
    ///     Higher temperature results in more randomness.
    ///     Range: (0.00, 2.00)
    ///     Default Value: 0.9
    /// </summary>
    public float? Temperature { get; set; }

    public double? TopP { get; set; }
    public double? PresencePenalty { get; set; }
    public double? FrequencyPenalty { get; set; }

    /// <summary>
    ///     Assistant ID
    /// </summary>
    public Guid? AssistantId { get; set; }
}