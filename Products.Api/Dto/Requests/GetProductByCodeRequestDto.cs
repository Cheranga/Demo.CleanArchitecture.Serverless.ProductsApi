using MediatR;
using Products.Api.Dto.Responses;
using Products.Domain;

namespace Products.Api.Dto.Requests
{
    public class GetProductByCodeRequestDto : IRequest<Result<GetProductResponseDto>>, IValidatableRequest
    {
        public string ProductCode { get; set; }
    }
}