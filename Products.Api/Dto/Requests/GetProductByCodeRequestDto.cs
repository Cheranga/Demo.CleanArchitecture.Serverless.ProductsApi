using MediatR;
using Products.Api.Dto.Responses;
using Products.Domain;

namespace Products.Api.Dto.Requests
{
    public class GetProductByCodeRequestDto : IRequest<Result<GetProductResponseDto>>, IValidatable
    {
        public string ProductCode { get; set; }
        public string CorrelationId { get; set; }
    }
}