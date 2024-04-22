namespace ChatUAISDK.Requests;

public class CreateImageRequest
{
    /// <summary>
    /// prompt 
    /// </summary>
    public string Prompt { get; set; }

    /// <summary>
    /// style : enhance, style, cartoon, colorize
    /// </summary>
    public string Style { get; set; }

    /// <summary>
    /// count: number of images (1-4)
    /// </summary>
    public int? Count { get; set; }

    /// <summary>
    /// Whether to optimize and translate the prompt words
    /// </summary>
    public bool? PromptOptimize { get; set; }
}