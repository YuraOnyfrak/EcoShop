using Project.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeToCommand<TCommnand>() 
            where TCommnand : ICommand;
    }
}
