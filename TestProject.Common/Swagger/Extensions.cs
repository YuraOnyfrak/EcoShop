using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TestProject.Common.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            SwaggerOptions options;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                options = configuration.GetSection("swagger").Get<SwaggerOptions>();
            }

            if (!options.Enabled)
                return services;

            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version });
                if (options.IncludeSecurity)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            new List<string>()
                        }
                    });

                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, 
                        $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
                    c.CustomSchemaIds(x => x.FullName);
                }
            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<IConfiguration>()
                .GetSection("swagger").Get<SwaggerOptions>();

            if (!options.Enabled)
                return builder;

            builder.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            var env = builder.ApplicationServices.GetService<IWebHostEnvironment>();

            if (env.IsDevelopment())
                builder.Use(async (context, next) =>
                {
                    if (context.Request.Path.Value == "/")
                    {
                        context.Response.Redirect("/swagger", true);
                    }
                    else await next();
                });

            return builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", $"{Assembly.GetEntryAssembly().GetName().Name} API V1");
            });
        }
    }
}