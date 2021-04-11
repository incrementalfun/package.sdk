using System;
using Incremental.Common.SDK.Helpers;
using Incremental.Common.SDK.Options;
using MassTransit;
using MassTransit.AmazonSqsTransport;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Incremental.Common.SDK
{
    /// <summary>
    /// Extensions to <see cref="IServiceCollection"/> to configure SDKs.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures an SDK in an application. Meant to be used by individual SDKs.
        /// Don't call this if you don't really know what you are doing.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="busConfiguration"></param>
        /// <param name="messageConfigurators"></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSdk(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollectionBusConfigurator> busConfiguration, params Action<IBusRegistrationContext, IAmazonSqsBusFactoryConfigurator>[] messageConfigurators)
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
                
                busConfiguration.Invoke(configurator);
            });
            
            return services;
        }

        public static IServiceCollection ConfigureSdkOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string section) where TOptions : SdkOptions
        {
            return services.Configure<TOptions>(configuration.GetSection(SdkOptions.Sdk).GetSection(section));
        }
    }
}