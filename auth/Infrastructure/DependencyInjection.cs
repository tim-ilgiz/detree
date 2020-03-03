using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection (this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DB_CONNECTION_STRING"),
                    b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName)));

            return services;
        }
    }
}