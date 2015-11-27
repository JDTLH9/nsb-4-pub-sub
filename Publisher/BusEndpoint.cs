using NServiceBus;
using StructureMap;

namespace Publisher
{
    public class BusEndpoint : IConfigureThisEndpoint
    {
        public const string ENDPOINT_NAME = "Tech.Talks";

        public static void RunBus()
        {
            SetupEndpoint().Initialize();
        }

        public static Configure SetupEndpoint()
        {
            Configure.Endpoint.AsSendOnly();
            Configure.Transactions.Disable();

            Configure.Features.Disable<NServiceBus.Features.AutoSubscribe>();
            Configure.Features.Disable<NServiceBus.Features.SecondLevelRetries>();
            Configure.Features.Disable<NServiceBus.Features.TimeoutManager>();
            Configure.Features.Disable<NServiceBus.Features.Gateway>();
            Configure.Features.Disable<NServiceBus.Features.Sagas>();

            Configure.Serialization.Xml();

            return Configure.With(AllAssemblies.Matching("Messages.dll"))
                .DefineEndpointName(ENDPOINT_NAME)
                .DefiningCommandsAs(t => t.Namespace != null &&
                                         t.Namespace.Contains("Messages.") &&
                                         !t.Namespace.StartsWith("NServiceBus"))
                .StructureMapBuilder(ObjectFactory.Container)
                .UseTransport<Msmq>()
                .UnicastBus();
        }
    }
}