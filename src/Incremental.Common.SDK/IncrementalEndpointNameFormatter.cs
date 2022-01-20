using System;
using System.Reflection;
using System.Text;
using Incremental.Common.SDK.Options;
using MassTransit.Definition;

namespace Incremental.Common.SDK;

internal class IncrementalEndpointNameFormatter
{
    public static string PrefixBuilder(SdkOptions options)
    {
        var prefix = new StringBuilder();

        if (options.Prefix is not null)
        {
            prefix.Append(options.Prefix);
        }

        if (options.IncludeEnvironment)
        {
            prefix.Append('_');
            prefix.Append(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown");
        }

        return prefix.ToString();
    }

    public static string QueueNameForEntryAssembly(SdkOptions options)
    {
        return $"{PrefixBuilder(options)}_{Assembly.GetEntryAssembly()?.GetName().Name?.Replace('.', '_') ?? "Unknown"}";
    }
        
    public static SnakeCaseEndpointNameFormatter Instance(SdkOptions options)
    {
        return new SnakeCaseEndpointNameFormatter(PrefixBuilder(options), options.IncludeNamespace);
    }
}
