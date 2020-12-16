using EcoShop.Common.Common;
using EcoShop.Common.Messages;
using Project.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoShop.Common.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) 
            where TCommand : ICommand;

        Task PublishAsync<TCommand>(TCommand @event, ICorrelationContext context)
             where TCommand : IEvent;
    }
}
