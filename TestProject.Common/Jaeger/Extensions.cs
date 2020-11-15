using EcoShop.Common.Common.Extensions;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using RawRabbit.Instantiation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace EcoShop.Common.Jaeger
{
    public static class Extensions 
    {
        public static IServiceCollection AddJaeger(this IServiceCollection services, IConfiguration configuration)
        {
            var jaegerOptions = configuration.GetSection(nameof(JaegerOptions).GetNameOptions()).Get<JaegerOptions>();

            if (!jaegerOptions.Enabled)
            {
                services.AddSingleton<ITracer>(sp =>
                {
                    var tracer = new Tracer.Builder(Assembly.GetEntryAssembly().FullName)
                        .WithReporter(new NoopReporter())
                        .WithSampler(new ConstSampler(false))
                        .Build();

                    return tracer;
                });

                return services;
            }

            services.AddSingleton<ITracer>(sp =>
                {
                    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                    var reporter = new RemoteReporter.Builder()
                        .WithSender(new UdpSender(jaegerOptions.UdpHost, jaegerOptions.UdpPort, jaegerOptions.MaxPacketSize))
                        .WithLoggerFactory(loggerFactory)
                        .Build();

                    var sampler = GetSampler(jaegerOptions);

                    var tracer = new Tracer.Builder(jaegerOptions.ServiceName)
                        .WithReporter(reporter)
                        .WithSampler(sampler)
                        .Build();

                    if (!GlobalTracer.IsRegistered())
                        GlobalTracer.Register(tracer);

                    return tracer;
                });

            return services;            
        }

        public static IClientBuilder UseJaeger(this IClientBuilder builder, ITracer tracer)
        {
            builder.Register(pipe => pipe
                .Use<JaegerStagedMiddleware>(tracer));
            return builder;
        }

        private static ISampler GetSampler(JaegerOptions options)
        {
            switch (options.Sampler)
            {
                case "const": return new ConstSampler(true);
                case "rate": return new RateLimitingSampler(options.MaxTracesPerSecond);
                case "probabilistic": return new ProbabilisticSampler(options.SamplingRate);
                default: return new ConstSampler(true);
            }
        }
    }
}
