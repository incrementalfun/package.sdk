using System;
using Incremental.Common.Messaging;
using Incremental.Common.SDK.Options;
using MassTransit;
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
        /// Configures an SDK in an application.
        /// Don't call this if you don't really know what you are doing.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="busConfiguration"></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSdk(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollectionBusConfigurator> busConfiguration)
        {
            services.AddMessaging();

            services.AddMassTransit(configurator =>
            {
                configurator.SetEndpointNameFormatter(IncrementalEndpointNameFormatter.Instance());
                
                configurator.UsingAmazonSqs((ctx, cfg) => 
                {
                    cfg.Host("eu-west-1", host =>
                    {
                        host.AccessKey(configuration["AWS_ACCESS_KEY"]);
                        host.SecretKey(configuration["AWS_SECRET_KEY"]);

                        var scope = configuration["SDK_SCOPE"] != null 
                            ? configuration["SDK_SCOPE"] 
                            : "Incremental";
                        
                        host.Scope($"${scope}_{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"}");
                        host.EnableScopedTopics();
                    });
                    
                    cfg.ConfigureEndpoints(ctx);
                });
                
                busConfiguration.Invoke(configurator);
            });

            services.AddGenericRequestClient();
            
            return services;
        }

        /// <summary>
        ///     Helper method to configure SDK options.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <typeparam name="TOptions"></typeparam>
        /// <returns></returns>
        public static IServiceCollection ConfigureSdkOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string section) where TOptions : SdkOptions
        {
            return services.Configure<TOptions>(configuration.GetSection(SdkOptions.Sdk).GetSection(section));
        }
    }
}