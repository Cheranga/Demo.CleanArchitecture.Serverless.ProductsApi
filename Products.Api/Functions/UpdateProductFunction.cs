using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Products.Api.Dto.Requests;
using Products.Api.Dto.Responses;
using Products.Api.Extensions;

namespace Products.Api.Functions
{
    public class UpdateProductFunction
    {
        private readonly IMediator _mediator;

        public UpdateProductFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName(nameof(UpdateProductFunction))]
        [OpenApiOperation("UpdateProduct", "product", Summary = "Update product.", Description = "This will update a product.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody("application/json", typeof(UpsertProductRequestDto), Required = true, Description = "The product data which needs to be updated.")]
        [OpenApiParameter("correlationId", In = ParameterLocation.Header, Required = true, Description = "The correlaion id of the operation")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(UpsertProductResponseDto), Summary = "The product details.", Description = "The product details which was updated.")]
        [OpenApiResponseWithoutBody(HttpStatusCode.MethodNotAllowed, Summary = "Invalid input", Description = "Invalid input")]

        public async Task<IActionResult> InsertProduct([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products")]
            HttpRequest request)
        {
            var correlationId = request.GetHeaderValue("correlationId");
            var updateProductRequestDto = await request.ToModel<UpsertProductRequestDto>(dto => dto.CorrelationId = correlationId);

            var operation = await _mediator.Send(updateProductRequestDto);

            if (operation.Status)
            {
                return new OkResult();
            }

            return new BadRequestObjectResult(operation);
        }
    }
}