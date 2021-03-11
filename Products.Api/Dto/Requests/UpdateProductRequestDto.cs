using MediatR;
using Products.Api.Dto.Responses;
using Products.Domain;

namespace Products.Api.Dto.Requests
{
    public class UpdateProductRequestDto : IRequest<Result<UpdateProductResponseDto>>, IValidatable
    {
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string CorrelationId { get; set; }
    }
}