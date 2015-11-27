using StructureMap;

namespace Publisher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new PublisherRegistry());
            });

            BusEndpoint.SetupEndpoint().Initialize();
            var sender = ObjectFactory.GetInstance<ISender>();
            sender.Send();
        }
    }
}
