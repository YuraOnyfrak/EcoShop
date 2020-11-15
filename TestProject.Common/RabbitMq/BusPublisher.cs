using EcoShop.Common.Messages;
using Project.Common.Common;
using Project.Common.Messages;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Common.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand
            => await _busClient.PublishAsync(command, ctx => ctx.UseMessageContext(context));

        public async Task PublishAsync<TCommand>(TCommand @event, ICorrelationContext context)
            where TCommand : IEvent
            => throw new NotImplementedException();
    }
}
