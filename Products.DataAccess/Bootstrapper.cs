using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Products.DataAccess
{
    public static class Bootstrapper
    {
        public static void UseProductsDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
            }

            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.Configure<DatabaseConfig>(configuration.GetSection("Values:DatabaseConfig"));
            services.AddScoped(provider =>
            {
                var config = provider.GetRequiredService<IOptionsSnapshot<DatabaseConfig>>().Value;
                return config;
            });
        }
    }
}