using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Application.Requests;
using Products.Application.Responses;
using Products.Domain;
using Products.Domain.Queries;

namespace Products.Application.Handlers
{
    public class GetProductByCodeRequestHandler : IRequestHandler<GetProductByCodeRequest, Result<GetProductResponse>>
    {
        private readonly IMediator _mediator;

        public GetProductByCodeRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetProductResponse>> Handle(GetProductByCodeRequest request, CancellationToken cancellationToken)
        {
            var getProductByCodeQuery = new GetProductByCodeQuery
            {
                CorrelationId = request.CorrelationId,
                Code = request.Code
            };

            var operation = await _mediator.Send(getProductByCodeQuery, cancellationToken);
            if (!operation.Status)
            {
                return Result<GetProductResponse>.Failure(operation.Validation);
            }

            var product = operation.Data;
            if (product == null)
            {
                return Result<GetProductResponse>.Success(null);
            }

            var response = new GetProductResponse();
            return Result<GetProductResponse>.Success(response);
        }
    }
}