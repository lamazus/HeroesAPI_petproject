using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                  //  x => x.MigrationsAssembly("Infrastructure"));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            return services;
        }
    }
}
