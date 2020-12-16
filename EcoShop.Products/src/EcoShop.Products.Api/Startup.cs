using Consul;
using EcoShop.Common.Dispatchers;
using DShop.Common.Fabio;
using EcoShop.Common.Jaeger;
using EcoShop.Common.RabbitMq;
using EcoShop.Products.Application;
using EcoShop.Products.Infastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Common.Handlers;
using TestProject.Common.Consul;
using TestProject.Common.Mvc;
using TestProject.Common.Swagger;
using EcoShop.Products.Application.Messages.Commands;
using EcoShop.Products.Application.Handler.Commands;
using EcoShop.Products.Domain.Repositorty;
using EcoShop.Products.Domain.Entity;
using EcoShop.Products.Infastructure.Repository;
using EcoShop.Products.Infastructure.Persistance;

namespace EcoShop.Products.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddInfastructure(Configuration);
            services.AddApplication();
            services.AddSwaggerDocs();
            services.AddSingleton<IServiceId, ServiceId>();

            services.AddConsul();
            services.AddFabio();
            services.AddJaeger(Configuration);
            services.AddOpenTracing();
            services.AddRabbitMq(Configuration);
            services.AddDispatchers();
            services.AddTransient<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
          
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IAggregateRootRepository<Product>, AggregateRootRepository<Product>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
             IHostApplicationLifetime applicationLifetime, IConsulClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwaggerDocs();

            if (env.IsDevelopment())
                app.Use(async (context, next) =>
                {
                    if (context.Request.Path.Value == "/")
                    {
                        context.Response.Redirect("/swagger", true);
                    }
                    else await next();
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitMq();

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
            });
        }
    }
}
