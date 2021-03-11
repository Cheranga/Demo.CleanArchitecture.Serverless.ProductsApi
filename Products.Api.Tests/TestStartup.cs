using System;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Api.Dto.Requests;
using Products.Api.Dto.Responses;
using Products.Domain;
using Products.Domain.Behaviours;

namespace Products.Api.Tests
{
    public class TestStartup : Startup
    {
        protected override IConfigurationRoot GetConfigurationRoot(IFunctionsHostBuilder functionsHostBuilder)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("local.settings.json")
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            //
            // If required override the dependencies here
            //
            //var services = builder.Services;
            //services.AddTransient<IPipelineBehavior<InsertProductRequestDto, Result<InsertProductResponseDto>>, ProductMicroserviceValidationBehaviour<InsertProductRequestDto, InsertProductResponseDto>>();
            //services.AddTransient<IPipelineBehavior<InsertProductRequestDto, Result<InsertProductResponseDto>>, ProductMicroservicePerformanceBehaviour<InsertProductRequestDto, InsertProductResponseDto>>();


        }

        protected override void RegisterMediatr(IServiceCollection services)
        {
            var mediatrAssemblies = new[] { typeof(Startup).Assembly, typeof(Bootstrapper).Assembly };

            services.AddMediatR(mediatrAssemblies);
            services.AddTransient<IPipelineBehavior<InsertProductRequestDto, Result<InsertProductResponseDto>>, ProductMicroserviceValidationBehaviour<InsertProductRequestDto, InsertProductResponseDto>>();
            services.AddTransient<IPipelineBehavior<InsertProductRequestDto, Result<InsertProductResponseDto>>, ProductMicroservicePerformanceBehaviour<InsertProductRequestDto, InsertProductResponseDto>>();
        }
    }
}