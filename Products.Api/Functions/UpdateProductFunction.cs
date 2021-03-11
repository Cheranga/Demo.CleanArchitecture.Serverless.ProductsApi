using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Products.Api.Dto.Requests;
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