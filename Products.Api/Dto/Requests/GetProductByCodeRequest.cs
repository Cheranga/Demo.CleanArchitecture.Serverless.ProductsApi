using MediatR;
using Products.Api.Dto.Responses;
using Products.Domain;

namespace Products.Api.Dto.Requests
{
    public class GetProductByCodeRequest : IRequest<Result<GetProductResponse>>, IValidatableRequest
    {
        public string ProductCode { get; set; }
    }
}