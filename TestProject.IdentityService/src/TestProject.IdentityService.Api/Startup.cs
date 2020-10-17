using Consul;
using DShop.Common.Fabio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.Common.Authentication;
using TestProject.Common.Consul;
using TestProject.Common.Mvc;
using TestProject.Common.Swagger;
using TestProject.IdentityService.Application;
using TestProject.IdentityService.Infastructure;

namespace TestProject.IdentityService.Api
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
            services.AddJwt();
            services.AddInfastructure(Configuration);
            services.AddApplication();
            services.AddSwaggerDocs();
            services.AddSingleton<IServiceId, ServiceId>();

            services.AddConsul();
            services.AddFabio();
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

            app.UseAuthentication();
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

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
            });
        }
    }
}
