using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Behaviours;

namespace Products.Domain
{
    public static class Bootstrapper
    {
        public static void UseDomain(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ProductMicroserviceValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ProductMicroservicePerformanceBehaviour<,>));
        }
    }
}