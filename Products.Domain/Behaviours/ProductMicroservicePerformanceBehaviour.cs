using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Products.Domain.Behaviours
{
    public class ProductMicroservicePerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>> where TRequest : IValidatableRequest
    {
        private readonly ILogger<ProductMicroservicePerformanceBehaviour<TRequest, TResponse>> _logger;

        public ProductMicroservicePerformanceBehaviour(ILogger<ProductMicroservicePerformanceBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var response = await next();

            stopWatch.Stop();

            _logger.LogInformation("Handling of {request} ended, time taken {timeTaken} ms.", typeof(TRequest).Name, stopWatch.ElapsedMilliseconds);

            return response;
        }
    }
}