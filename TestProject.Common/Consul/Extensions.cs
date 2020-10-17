using System;
using Consul;
using DShop.Common.Fabio;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestProject.Common.Consul;
using TestProject.Common.Fabio;
using TestProject.Common.Mvc;

namespace TestProject.Common.Consul
{
    public static class Extensions
    {
        private static readonly string ConsulSectionName = "consul";
        private static readonly string FabioSectionName = "fabio";

        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var options = configuration.GetSection(ConsulSectionName).Get<ConsulOptions>();

            //services.ConfigureOptions<FabioOptions>();
            //services.ConfigureOptions<ConsulOptions>();
            services.AddTransient<IConsulServicesRegistry, ConsulServicesRegistry>();
            services.AddTransient<ConsulServiceDiscoveryMessageHandler>();
            services.AddHttpClient<IConsulHttpClient, ConsulHttpClient>()
                .AddHttpMessageHandler<ConsulServiceDiscoveryMessageHandler>();

            return services.AddSingleton<IConsulClient>(c => new ConsulClient(cfg =>
            {
                if (!string.IsNullOrEmpty(options.Url))
                {
                    cfg.Address = new Uri(options.Url);
                }
            }));
        }

        //Returns unique service ID used for removing the service from registry.
        public static string UseConsul(this IApplicationBuilder app)
        {
            IConfiguration configuration;

            using (var scope = app.ApplicationServices.CreateScope())
            {
                configuration = scope.ServiceProvider.GetService<IConfiguration>();
                var consulOptions = configuration.GetSection(ConsulSectionName).Get<ConsulOptions>();
                var fabioOptions = configuration.GetSection(FabioSectionName).Get<FabioOptions>();
                var enabled = consulOptions.Enabled;
                var consulEnabled = Environment.GetEnvironmentVariable("CONSUL_ENABLED")?.ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(consulEnabled))
                {
                    enabled = consulEnabled == "true" || consulEnabled == "1";
                }

                if (!enabled)
                {
                    return string.Empty;
                }


                var address = consulOptions.Address;
                if (string.IsNullOrWhiteSpace(address))
                {
                    throw new ArgumentException("Consul address can not be empty.",
                        nameof(consulOptions.PingEndpoint));
                }

                var uniqueId = scope.ServiceProvider.GetService<IServiceId>().Id;
                var client = scope.ServiceProvider.GetService<IConsulClient>();
                var serviceName = consulOptions.Service;
                var serviceId = $"{serviceName}:{uniqueId}";
                var port = consulOptions.Port;
                var pingEndpoint = consulOptions.PingEndpoint;
                var pingInterval = consulOptions.PingInterval <= 0 ? 5 : consulOptions.PingInterval;
                var removeAfterInterval =
                    consulOptions.RemoveAfterInterval <= 0 ? 10 : consulOptions.RemoveAfterInterval;
                var registration = new AgentServiceRegistration
                {
                    Name = serviceName,
                    ID = serviceId,
                    Address = address,
                    Port = port,
                    Tags = fabioOptions.Enabled ? GetFabioTags(serviceName, fabioOptions.Service) : null
                };
                if (consulOptions.PingEnabled || fabioOptions.Enabled)
                {
                    var scheme = address.StartsWith("http", StringComparison.InvariantCultureIgnoreCase)
                        ? string.Empty
                        : "http://";
                    var check = new AgentServiceCheck
                    {
                        Interval = TimeSpan.FromSeconds(pingInterval),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(removeAfterInterval),
                        HTTP = $"{scheme}{address}{(port > 0 ? $":{port}" : string.Empty)}/{pingEndpoint}"
                    };
                    registration.Checks = new[] {check};
                }

                client.Agent.ServiceRegister(registration);

                return serviceId;
            }
        }

        private static string[] GetFabioTags(string consulService, string fabioService)
        {
            var service = (string.IsNullOrWhiteSpace(fabioService) ? consulService : fabioService)
                .ToLowerInvariant();

            return new[] {$"urlprefix-/{service} strip=/{service}"};
        }
    }
}