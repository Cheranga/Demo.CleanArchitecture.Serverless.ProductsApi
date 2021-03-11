using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Behaviours;

namespace Products.Domain
{
    public static class Bootstrapper
    {
        public static void UseDomain(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        }
    }
}