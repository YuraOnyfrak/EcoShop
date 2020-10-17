using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Common.Authentication
{
    public static class Extensions
    {
        private static readonly string SectionName = "jwt";

        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var options = configuration.GetSection(SectionName).Get<JwtOptions>();

            services.AddAuthentication()
               .AddJwtBearer(cfg =>
               {                   
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                      // ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                       //ValidateIssuer = true,
                       ValidIssuer = options.Issuer,
                       ValidAudience = options.ValidAudience,
                       ValidateAudience = options.ValidateAudience,
                       ValidateLifetime = options.ValidateLifetime
                   };
               });                            

            return services;
        }
    }
}
