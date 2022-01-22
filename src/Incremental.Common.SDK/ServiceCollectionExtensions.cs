using System;
using Incremental.Common.Messaging;
using Incremental.Common.SDK.Options;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Incremental.Common.SDK;

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

        services.Configure<SdkOptions>(configuration.GetSection(SdkOptions.Sdk));

        var options = GetSdkOptions(configuration);
        
        services.AddMassTransit(configurator =>
        {
            busConfiguration.Invoke(configurator);

            configurator.SetEndpointNameFormatter(IncrementalEndpointNameFormatter.Instance(options));
                
            configurator.UsingAmazonSqs((ctx, cfg) =>
            {
                cfg.Host("eu-west-1", host =>
                {
                    host.AccessKey(configuration["AWS_ACCESS_KEY"]);
                    host.SecretKey(configuration["AWS_SECRET_KEY"]);

                    host.Scope(IncrementalEndpointNameFormatter.PrefixBuilder(options), true);
                    host.EnableScopedTopics();
                });

                cfg.ReceiveEndpoint(IncrementalEndpointNameFormatter.QueueNameForEntryAssembly(options), endpointCfg => 
                {
                    endpointCfg.ConfigureConsumers(ctx); 
                });
            });
        });

        services.AddGenericRequestClient();
            
        return services;
    }

    private static SdkOptions GetSdkOptions(IConfiguration configuration)
    {
        var options = new SdkOptions();
        configuration.GetSection(SdkOptions.Sdk).Bind(options);
        
        return options;
    }

    /// <summary>
    /// Configures options described in the SDK configuration namespace.
    /// Should not be called directly in any api.
    /// Used by individual SDKs.
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