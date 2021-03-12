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
using Products.Application.Requests;

namespace Products.Api.Functions
{
    public class InsertProductFunction
    {
        private readonly IMediator _mediator;

        public InsertProductFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName(nameof(InsertProductFunction))]
        [OpenApiOperation("InsertProduct", "product", Summary = "Insert product.", Description = "This will insert a new product.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody("application/json", typeof(UpsertProductRequestDto), Required = true, Description = "The product data which needs to be inserted as a new product.")]
        [OpenApiParameter("correlationId",In = ParameterLocation.Header, Required = true, Description = "The correlaion id of the operation")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(UpsertProductResponseDto), Summary = "The product details.", Description = "The product details which was inserted.")]
        [OpenApiResponseWithoutBody(HttpStatusCode.MethodNotAllowed, Summary = "Invalid input", Description = "Invalid input")]
        public async Task<IActionResult> InsertProduct([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")]
            HttpRequest request)
        {
            var correlationId = request.GetHeaderValue("correlationId");
            var insertProductRequestDto = await request.ToModel<UpsertProductRequestDto>(dto => dto.CorrelationId = correlationId);

            var operation = await _mediator.Send(insertProductRequestDto);

            if (operation.Status)
            {
                return new OkResult();
            }

            return new BadRequestObjectResult(operation);
        }
    }
}