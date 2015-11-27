using StructureMap.Configuration.DSL;

namespace Subscriber
{
    public class SubscriberRegistry : Registry
    {
        public SubscriberRegistry()
        {
            Configure(expression => expression.Scan(scan =>
            {
                scan.AssemblyContainingType<EndpointConfig>();
                scan.WithDefaultConventions();
            }));
        }
    }
}