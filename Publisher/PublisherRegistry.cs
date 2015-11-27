using StructureMap.Configuration.DSL;

namespace Publisher
{
    public class PublisherRegistry : Registry
    {
        public PublisherRegistry()
        {
            Configure(expression => expression.Scan(scan =>
            {
                scan.AssemblyContainingType<Sender>();
                scan.WithDefaultConventions();
            }));
        }
    }
}
