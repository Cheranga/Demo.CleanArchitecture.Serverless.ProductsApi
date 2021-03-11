using MediatR;
using Products.Domain;

namespace Products.Application
{
    public class InsertProductRequest : IRequest<Result<InsertProductResponse>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}