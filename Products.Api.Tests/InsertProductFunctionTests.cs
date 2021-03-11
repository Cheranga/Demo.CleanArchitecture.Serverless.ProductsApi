using System;
using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Products.Api.Dto.Requests;
using Products.Api.Functions;
using Xunit;

namespace Products.Api.Tests
{
    [Collection(IntegrationTestsCollection.Name)]
    public class InsertProductFunctionTests
    {
        private readonly TestsInitializer _testsInitializer;
        private IMediator _mediator;
        private ILogger<InsertProductFunction> _logger;
        private Fixture _fixture;


        public InsertProductFunctionTests(TestsInitializer testsInitializer)
        {
            _testsInitializer = testsInitializer;

            var serviceProvider = testsInitializer.ServiceProvider;
            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _logger = serviceProvider.GetRequiredService<ILogger<InsertProductFunction>>();
            _fixture = testsInitializer.Fixture;
        }

        [Fact]
        public async Task Test()
        {
            var function = new InsertProductFunction(_mediator, _logger);

            var dto = _fixture.Create<UpsertProductRequestDto>();
            dto.CorrelationId = string.Empty;

            var mockedHttpRequest = await GetMockedRequest(dto);

            var response = await function.InsertProduct(mockedHttpRequest);
        }

        private async Task<HttpRequest> GetMockedRequest<TData>(TData data) where TData : class
        {
            var serializedData = JsonConvert.SerializeObject(data);
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream) { AutoFlush = true };

            await streamWriter.WriteAsync(serializedData);
            memoryStream.Position = 0;

            var mockedRequest = new Mock<HttpRequest>();
            mockedRequest.Setup(x => x.Body).Returns(memoryStream);
            mockedRequest.Setup(x => x.Headers).Returns(new HeaderDictionary());

            return mockedRequest.Object;
        }
    }
}