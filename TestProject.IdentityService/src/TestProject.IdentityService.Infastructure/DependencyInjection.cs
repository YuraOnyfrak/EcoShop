using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.IdentityService.Application.Common.Interfaces;
using TestProject.IdentityService.Domain.Entity;
using TestProject.IdentityService.Infastructure.Persistance;

namespace TestProject.IdentityService.Infastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(Persistance.ApplicationDbContext)), options =>
                {
                    //options.UseRelationalNulls();
                    //options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
            })
            .AddIdentity<User, UserRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();            

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
