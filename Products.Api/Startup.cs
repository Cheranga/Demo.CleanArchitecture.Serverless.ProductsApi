using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Api;
using Products.Domain;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Products.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            var configuration = GetConfigurationRoot(builder);

            RegisterMediatr(services);
            RegisterValidators(services);
            DataAccess.Bootstrapper.UseProductsDataAccess(services, configuration);
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var validatorAssemblies = new[] {typeof(Startup).Assembly};

            services.AddValidatorsFromAssemblies(validatorAssemblies);

            // TODO: Add validators from application and dataaccess modules.
        }

        protected virtual void RegisterMediatr(IServiceCollection services)
        {
            var mediatrAssemblies = new[] {typeof(Startup).Assembly, typeof(Bootstrapper).Assembly, typeof(Application.Bootstrapper).Assembly, typeof(DataAccess.Bootstrapper).Assembly};

            services.AddMediatR(mediatrAssemblies);

            services.UseDomain();
        }

        protected virtual IConfigurationRoot GetConfigurationRoot(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            var executionContextOptions = services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(executionContextOptions.AppDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }
    }
}