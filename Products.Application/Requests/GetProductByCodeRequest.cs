using MediatR;
using Products.Application.Responses;
using Products.Domain;

namespace Products.Application.Requests
{
    public class GetProductByCodeRequest : IRequest<Result<GetProductResponse>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
    }
}