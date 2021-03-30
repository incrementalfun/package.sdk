using System;
using Incremental.Common.SDK.Helpers;
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
        public static IServiceCollection AddSdk(this IServiceCollection services, IConfiguration configuration, params Action<IBusRegistrationContext, IAmazonSqsBusFactoryConfigurator>[] messageConfigurators)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingAmazonSqs((ctx, cfg) => 
                {
                    cfg.Host("eu-west-1", host =>
                    {
                        host.AccessKey(configuration["AWS_ACCESS_KEY"]);
                        host.SecretKey(configuration["AWS_SECRET_KEY"]);

                        host.Scope(Topic.Scope());
                        host.EnableScopedTopics();
                    });

                    foreach (var messageConfigurator in messageConfigurators)
                    {
                        messageConfigurator.Invoke(ctx, cfg);
                    }
                });
            });
            
            return services;
        }
    }
}