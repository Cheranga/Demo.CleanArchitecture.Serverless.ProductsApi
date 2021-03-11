using MediatR;
using Products.Domain;

namespace Products.Application
{
    public class CreateProductCommand : IRequest<Result<Product>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}