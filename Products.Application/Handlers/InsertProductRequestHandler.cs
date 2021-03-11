using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Application.Requests;
using Products.Application.Responses;
using Products.Domain;
using Products.Domain.Commands;

namespace Products.Application.Handlers
{
    public class InsertProductRequestHandler : IRequestHandler<InsertProductRequest, Result<InsertProductResponse>>
    {
        private readonly IMediator _mediator;

        public InsertProductRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<InsertProductResponse>> Handle(InsertProductRequest request, CancellationToken cancellationToken)
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
                return Result<InsertProductResponse>.Success(new InsertProductResponse());
            }

            return Result<InsertProductResponse>.Failure(operation.Validation);
        }
    }
}