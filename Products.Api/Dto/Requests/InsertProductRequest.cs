using MediatR;
using Products.Api.Dto.Responses;
using Products.Domain;

namespace Products.Api.Dto.Requests
{
    public class InsertProductRequest : IRequest<Result<InsertProductResponse>>, IValidatableRequest
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
    }
}