using EcoShop.Common.Jaeger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using Project.Common.Common;
using Project.Common.Handlers;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace EcoShop.Common.RabbitMq
{
    public static class Extensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
            => new BusSubscriber(app);

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient(typeof(ICommandHandler<>));
            services.AddTransient<IHandler, Handler>();
            services.AddTransient<IBusPublisher, BusPublisher>();

            services.AddSingleton<IInstanceFactory>(context =>
            {
                IConfiguration configuration;
                using (var serviceProvider = services.BuildServiceProvider())
                {
                    configuration = serviceProvider.GetService<IConfiguration>();
                }
                var options = configuration.GetSection("rabbitMq").Get<RabbitMqOptions>();
                //var rawRabbitConfiguration = context.GetService<RawRabbitConfiguration>();
                var namingConventions = new CustomNamingConventions(options.Namespace);
                var tracer = context.GetRequiredService<ITracer>();

                return RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions
                {
                    DependencyInjection = ioc =>
                    {
                        ioc.AddSingleton(options);
                        //ioc.AddSingleton(configuration);
                        ioc.AddSingleton<INamingConventions>(namingConventions);
                        ioc.AddSingleton(tracer);
                    },
                    Plugins = p => p
                        .UseAttributeRouting()
                        .UseMessageContext<CorrelationContext>()
                        .UseContextForwarding()
                        .UseJaeger(tracer)
                });
            });

            services.AddTransient<IBusClient>(context => context.GetService<IInstanceFactory>().Create());

        }
    }
}
