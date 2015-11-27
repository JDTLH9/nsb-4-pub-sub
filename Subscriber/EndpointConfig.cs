using StructureMap;

namespace Subscriber
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        private static IContainer _container;
        public const string ENDPOINT_NAME = "Subscriber";

        public void Init()
        {
            _container = new Container(new SubscriberRegistry());
            SetupEndpoint(_container);
        }

        public static Configure SetupEndpoint(IContainer container, string endpointName = ENDPOINT_NAME)
        {
            Configure.Transactions.Enable();

            var config = Configure.With(
                AllAssemblies.Matching("Subscriber.dll")
                    .And("Messages.dll")
                    .And("NServiceBus.NHibernate.dll"))
                .DefineEndpointName(endpointName)
                .DefiningMessagesAs(t => t.Namespace != null &&
                                         !t.Namespace.Contains("NServiceBus") &&
                                         t.Namespace.Contains("Messages.") &&
                                         t.Name.EndsWith("Message"))
                .DefiningEventsAs(
                    t =>
                        t.Namespace != null && !t.Namespace.Contains("NServiceBus")
                        && t.Namespace.Contains(".Messages.") && t.Name.EndsWith("Event"))
                .DefiningCommandsAs(t => t.Namespace != null &&
                                         !t.Namespace.Contains("NServiceBus") &&
                                         t.Namespace.Contains("Messages.") &&
                                         t.Name.EndsWith("Command"))
                .StructureMapBuilder(container)
                .UseNHibernateSagaPersister()
                .UseNHibernateTimeoutPersister()
                .UseNHibernateSubscriptionPersister()
                .UseTransport<Msmq>()
                .UnicastBus();

            return config;
        }
    }
}
