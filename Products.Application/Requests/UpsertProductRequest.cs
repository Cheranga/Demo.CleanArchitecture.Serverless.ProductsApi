using MediatR;
using Products.Application.Responses;
using Products.Domain;

namespace Products.Application.Requests
{
    public class UpsertProductRequest : IRequest<Result<UpsertProductResponse>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}