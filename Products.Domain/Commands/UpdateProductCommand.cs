using MediatR;
using Products.Domain.Models;

namespace Products.Domain.Commands
{
    public class UpdateProductCommand : IRequest<Result<Product>>, IValidatable
    {
        public string CorrelationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}