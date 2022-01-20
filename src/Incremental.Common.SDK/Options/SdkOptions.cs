namespace Incremental.Common.SDK.Options;

/// <summary>
/// SDK options.
/// </summary>
public class SdkOptions
{
    /// <summary>
    ///     SDK Options key.
    /// </summary>
    public const string Sdk = "Sdk";

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SdkOptions()
    {
        IncludeNamespace = false;
        IncludeEnvironment = true;
    }
        
    /// <summary>
    /// Include namespace.
    /// </summary>
    public bool IncludeNamespace { get; set; }
        
    /// <summary>
    /// Include Environment.
    /// </summary>
    public bool IncludeEnvironment { get; set; }
        
    /// <summary>
    /// Custom, additional prefix.
    /// </summary>
    public string? Prefix { get; set; }
}