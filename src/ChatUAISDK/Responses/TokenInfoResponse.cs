namespace ChatUAISDK.Responses;

public class TokenInfoResponse
{
    /// <summary>
    ///     总点数消耗限制，如为-1则为不限
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    ///     已消耗点数
    /// </summary>
    public int Consumed { get; set; }

    /// <summary>
    ///     当前AccessToken过期时间，如果为永久则为 固定值  "9999-12-31T23:59:59.9999999"
    /// </summary>
    public DateTime ExpirationTime { get; set; }
}