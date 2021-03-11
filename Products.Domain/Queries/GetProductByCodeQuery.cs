using MediatR;
using Products.Domain.Models;

namespace Products.Domain.Queries
{
    public class GetProductByCodeQuery : IRequest<Result<Product>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
    }
}