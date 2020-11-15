using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using DShop.Common.Fabio;
using EcoShop.Common.Cache;
using EcoShop.Common.Jaeger;
using EcoShop.Common.RabbitMq;
using EcoShop.Marketplace.Application;
using EcoShop.Marketplace.Application.Handler.Commands;
using EcoShop.Marketplace.Application.Messages.Commnads;
using EcoShop.Marketplace.Application.Services.Entrepreneur;
using EcoShop.Marketplace.Application.Services.Product;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Common.Handlers;
using TestProject.Common.Consul;
using TestProject.Common.Mvc;
using TestProject.Common.RestEase;
using TestProject.Common.Swagger;

namespace EcoShop.Marketplace.Api
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
            services.AddCache(Configuration);
            services.RegisterServiceForwarder<IProductService>("product-service");
            services.RegisterServiceForwarder<IEntrepreneurService>("entrepreneur-service");

            //services.AddInfastructure(Configuration);
            services.AddApplication();
            services.AddSwaggerDocs();
            services.AddSingleton<IServiceId, ServiceId>();

            services.AddConsul();
            services.AddFabio();
            services.AddRabbitMq(Configuration);
            services.AddJaeger(Configuration);
            services.AddOpenTracing();

            services.AddTransient(typeof(ICommandHandler<ProductCreatedCommand>), typeof(ProductCreatedCommandHandler));

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
            app.UseRabbitMq()
                .SubscribeToCommand<ProductCreatedCommand>();

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
            });
        }
    }
}
