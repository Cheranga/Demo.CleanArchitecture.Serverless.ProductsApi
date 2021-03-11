using MediatR;
using Products.Domain;

namespace Products.Application
{
    public class GetProductByCodeQuery : IRequest<Result<Product>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
    }
}