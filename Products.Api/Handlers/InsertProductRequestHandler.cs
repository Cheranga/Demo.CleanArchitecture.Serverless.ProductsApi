using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Dto.Requests;
using Products.Api.Dto.Responses;
using Products.Application;
using Products.Application.Requests;
using Products.Domain;

namespace Products.Api.Handlers
{
    public class InsertProductRequestHandler : IRequestHandler<InsertProductRequestDto, Result<InsertProductResponseDto>>
    {
        private readonly IMediator _mediator;

        public InsertProductRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<InsertProductResponseDto>> Handle(InsertProductRequestDto request, CancellationToken cancellationToken)
        {
            var insertProductRequest = new InsertProductRequest
            {
                CorrelationId = request.CorrelationId,
                Code = request.ProductCode,
                Description = request.ProductDescription
            };

            var operation = await _mediator.Send(insertProductRequest, cancellationToken);

            if (operation.Status)
            {
                return Result<InsertProductResponseDto>.Success(new InsertProductResponseDto());
            }

            return Result<InsertProductResponseDto>.Failure(operation.Validation);
        }
    }
}