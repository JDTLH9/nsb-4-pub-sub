using System;
using Messages.Commands;
using NServiceBus;

namespace Publisher
{
    public class Sender : ISender
    {
        private readonly IBus _bus;

        public Sender(IBus bus)
        {
            _bus = bus;
        }

        public void Send()
        {
            var msg = new MyMessage { MyIdGuid = Guid.NewGuid() };

            Console.WriteLine(msg.MyIdGuid);
            _bus.Send(msg);
        }
    }

    public interface ISender
    {
        void Send();
    }
}