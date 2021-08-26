using System;
using System.Reflection;
using MassTransit.Definition;

namespace Incremental.Common.SDK
{
    internal class IncrementalEndpointNameFormatter : SnakeCaseEndpointNameFormatter
    {
        public IncrementalEndpointNameFormatter(bool includeNamespace) : base(includeNamespace)
        {
        }

        public IncrementalEndpointNameFormatter(string prefix, bool includeNamespace) : base(prefix, includeNamespace)
        {
        }

        public IncrementalEndpointNameFormatter(char separator, string prefix, bool includeNamespace) : base(separator, prefix,
            includeNamespace)
        {
        }

        protected IncrementalEndpointNameFormatter()
        {
        }

        public new static IncrementalEndpointNameFormatter Instance() => new();

        public override string SanitizeName(string name)
        {
            return $"{Assembly.GetEntryAssembly()?.GetName().Name}_{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"}_{base.SanitizeName(name)}";
        }
    }
}