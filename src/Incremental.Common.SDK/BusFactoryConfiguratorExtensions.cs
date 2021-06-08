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
        public static void SetExternalEventConvention<TEvent>(this IBusFactoryConfigurator configurator) where TEvent : ExternalEvent
        {
            configurator.Message<TEvent>(t =>
                t.SetEntityName(configurator.MessageTopology.EntityNameFormatter.FormatEntityName<TEvent>()));
        }
    }
}