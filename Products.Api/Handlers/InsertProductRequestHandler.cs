using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Dto.Requests;
using Products.Api.Dto.Responses;
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
            await Task.Delay(TimeSpan.FromSeconds(1));

            // TODO: Mapping and calling the sevice request.
            return Result<InsertProductResponseDto>.Success(new InsertProductResponseDto());
        }
    }
}