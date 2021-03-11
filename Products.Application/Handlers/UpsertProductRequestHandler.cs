using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Application.Requests;
using Products.Application.Responses;
using Products.Domain;
using Products.Domain.Commands;
using Products.Domain.Queries;

namespace Products.Application.Handlers
{
    public class UpsertProductRequestHandler : IRequestHandler<UpsertProductRequest, Result<UpsertProductResponse>>
    {
        private readonly IMediator _mediator;

        public UpsertProductRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<UpsertProductResponse>> Handle(UpsertProductRequest request, CancellationToken cancellationToken)
        {
            var getProductQuery = new GetProductByCodeQuery
            {
                CorrelationId = request.CorrelationId,
                Code = request.Code
            };

            var getProductOperation = await _mediator.Send(getProductQuery, cancellationToken);
            if (!getProductOperation.Status)
            {
                return Result<UpsertProductResponse>.Failure(getProductOperation.Validation);
            }

            var product = getProductOperation.Data;
            if (product == null)
            {
                return await CreateProductAsync(request, cancellationToken);
            }

            return await UpdateProductAsync(request, cancellationToken);
        }

        private async Task<Result<UpsertProductResponse>> CreateProductAsync(UpsertProductRequest request, CancellationToken cancellationToken)
        {
            var createProductCommand = new CreateProductCommand
            {
                CorrelationId = request.CorrelationId,
                Code = request.Code,
                Name = request.Description
            };

            var operation = await _mediator.Send(createProductCommand, cancellationToken);

            if (operation.Status)
            {
                return Result<UpsertProductResponse>.Success(new UpsertProductResponse());
            }

            return Result<UpsertProductResponse>.Failure(operation.Validation);
        }

        private async Task<Result<UpsertProductResponse>> UpdateProductAsync(UpsertProductRequest request, CancellationToken cancellationToken)
        {
            var updateProductCommand = new UpdateProductCommand
            {
                CorrelationId = request.CorrelationId,
                Code = request.Code,
                Name = request.Description
            };

            var operation = await _mediator.Send(updateProductCommand, cancellationToken);

            if (operation.Status)
            {
                return Result<UpsertProductResponse>.Success(new UpsertProductResponse());
            }

            return Result<UpsertProductResponse>.Failure(operation.Validation);
        }
    }
}