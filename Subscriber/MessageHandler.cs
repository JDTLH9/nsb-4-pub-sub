using System;
using Messages.Commands;
using NServiceBus;

namespace Subscriber
{
    public class MessageHandler : IHandleMessages<MyMessage>
    {
        public void Handle(MyMessage message)
        {
            Console.WriteLine(message.MyIdGuid);
        }
    }
}
