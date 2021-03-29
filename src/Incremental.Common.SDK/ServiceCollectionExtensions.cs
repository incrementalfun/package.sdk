using System;
using MassTransit;
using MassTransit.AmazonSqsTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Incremental.Common.SDK
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures an SDK in an application. Meant to be used by individual SDKs.
        /// Don't call this if you don't really know what you are doing.
        /// </summary>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSdk(this IServiceCollection services, IConfiguration configuration, params Action<IAmazonSqsBusFactoryConfigurator>[] messageConfigurators)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingAmazonSqs((_, cfg) =>
                {
                    cfg.Host("eu-west-1", host =>
                    {
                        host.AccessKey(configuration["AWS_ACCESS_KEY"]);
                        host.SecretKey(configuration["AWS_SECRET_KEY"]);

                        host.Scope($"Incremental_{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}");
                        host.EnableScopedTopics();
                    });

                    foreach (var messageConfigurator in messageConfigurators)
                    {
                        messageConfigurator.Invoke(cfg);
                    }
                });
            });
            
            return services;
        }
    }
}