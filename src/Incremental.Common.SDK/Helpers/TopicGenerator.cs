using System;

namespace Incremental.Common.SDK.Helpers
{
    public static class Topic
    {
        public static string For<TMessage>(string sdk)
        {
            return $"{Scope()}_{sdk}_{typeof(TMessage).FullName?.Replace('.', '_')}";
        }

        public static string Scope()
        {
            return $"Incremental_{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}";
        }
    }
}