using MediatR;
using Products.Api.Dto.Responses;
using Products.Domain;

namespace Products.Api.Dto.Requests
{
    public class InsertProductRequestDto : IRequest<Result<InsertProductResponseDto>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
    }
}