using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain.Extensions;

namespace Products.Domain.Behaviours
{
    public class ProductMicroserviceValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>> where TRequest : IValidatableRequest
    {
        private readonly ILogger<ProductMicroserviceValidationBehaviour<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest> _validator;

        public ProductMicroserviceValidationBehaviour(IValidator<TRequest> validator, ILogger<ProductMicroserviceValidationBehaviour<TRequest, TResponse>> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            _logger.LogInformation("Validating {request}", typeof(TRequest).Name);

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join(", ", validationResult.ToErrorMessage());
                _logger.LogWarning("Validation error: {errors}", errorMessage);
                return Result<TResponse>.Failure(errorMessage);
            }

            var operation = await next();
            return operation;
        }
    }
}