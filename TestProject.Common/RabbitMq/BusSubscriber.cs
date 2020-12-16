using EcoShop.Common.Common;
using EcoShop.Common.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Project.Common.Handlers;
using Project.Common.Messages;
using RawRabbit;
using RawRabbit.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Common.Exceptions;

namespace EcoShop.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public IBusSubscriber SubscribeToCommand<TCommnand>() where TCommnand : ICommand
        { 

           _busClient.SubscribeAsync<TCommnand, CorrelationContext>(async (command, correlationContext) =>
           {
               var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommnand>>();

               return await TryHandleAsync<TCommnand>(command, correlationContext,
                   () => commandHandler.HandleAsync(command, correlationContext));
           });

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent, CorrelationContext>(async (@event, correlationContext) =>
            {
                var eventHandler = _serviceProvider.GetService<IEventHandler<TEvent>>();

                return await TryHandleAsync(@event, correlationContext,
                    () => eventHandler.HandleAsync(@event, correlationContext));
            });

            return this;
        }

        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message,
          CorrelationContext correlationContext,
          Func<Task> handle, Func<TMessage, Exception, IRejectedEvent> onError = null)
        {
            int _retries = 0;
            double _retryInterval = 0;
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retries, i => TimeSpan.FromSeconds(_retryInterval));

            return await policy.ExecuteAsync<Acknowledgement>(async () =>
            {
                try
                {
                    await handle();

                    return new Ack();
                }
                catch (Exception)
                {

                    throw;
                }
            });
        }
    }
}
