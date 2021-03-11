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
    public class UpsertProductRequestHandler : IRequestHandler<UpsertProductRequestDto, Result<UpsertProductResponseDto>>
    {
        private readonly IMediator _mediator;

        public UpsertProductRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<UpsertProductResponseDto>> Handle(UpsertProductRequestDto request, CancellationToken cancellationToken)
        {
            var upsertProductRequest = new UpsertProductRequest
            {
                CorrelationId = request.CorrelationId,
                Code = request.ProductCode,
                Description = request.ProductDescription
            };

            var operation = await _mediator.Send(upsertProductRequest, cancellationToken);

            if (operation.Status)
            {
                return Result<UpsertProductResponseDto>.Success(new UpsertProductResponseDto());
            }

            return Result<UpsertProductResponseDto>.Failure(operation.Validation);
        }
    }
}