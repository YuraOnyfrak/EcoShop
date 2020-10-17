using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.Common.Authentication;
using TestProject.Common.Mvc;
using TestProject.Common.RestEase;
using TestProject.Common.Swagger;
using TestProject.Services;

namespace TestProject
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
            services.AddJwt()
                    .AddSwaggerDocs()
                    .AddControllersWithViews()
                    .AddDefaultJsonOptions();            

            services.RegisterServiceForwarder<IFirstService>("second-service");
            services.RegisterServiceForwarder<IIdentityService>("identity-service");
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
