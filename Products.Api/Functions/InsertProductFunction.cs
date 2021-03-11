using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Products.Api.Dto.Requests;
using Products.Api.Extensions;

namespace Products.Api.Functions
{
    public class InsertProductFunction
    {
        private readonly ILogger<InsertProductFunction> _logger;
        private readonly IMediator _mediator;

        public InsertProductFunction(IMediator mediator, ILogger<InsertProductFunction> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(nameof(InsertProductFunction))]
        public async Task<IActionResult> InsertProduct([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")]
            HttpRequest request)
        {
            var correlationId = request.GetHeaderValue("correlationId");
            var insertProductRequestDto = await request.ToModel<InsertProductRequestDto>(dto => dto.CorrelationId = correlationId);

            var operation = await _mediator.Send(insertProductRequestDto);

            if (operation.Status)
            {
                return new OkResult();
            }

            return new BadRequestObjectResult(operation.ErrorMessage);
        }
    }
}