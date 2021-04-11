using Incremental.Common.Messaging;
using MassTransit;

namespace Incremental.Common.SDK
{
    public static class BusFactoryConfiguratorExtensions
    {
        public static void SetMessageConvention<TMessage>(this IBusFactoryConfigurator configurator) where TMessage : Message
        {
            configurator.Message<TMessage>(t =>
                t.SetEntityName(configurator.MessageTopology.EntityNameFormatter.FormatEntityName<TMessage>()));
        }
    }
}