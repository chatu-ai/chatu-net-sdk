using System;

namespace ChatUAISDK.Responses;

public class AskResponse
{
    /// <summary>
    /// Generate content
    /// </summary>
    public string Answer { get; set; } = null!;

    /// <summary>
    /// Conversation ID If not passed before, a new one will be generated
    /// </summary>
    public Guid? ConversationId { get; set; }

    /// <summary>
    ///     Current credits
    /// </summary>
    public long Token { get; set; }

    /// <summary>
    /// Return the model currently used by the current call
    /// </summary>
    public string Model { get; set; } = null!;

    /// <summary>
    ///     Current credits
    /// </summary>
    public long Credits { get; set; }

    /// <summary>
    ///     Request id of the current request
    /// </summary>
    public Guid? RequestId { get; set; }
}