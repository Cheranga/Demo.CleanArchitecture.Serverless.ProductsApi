using System;
using AutoFixture;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;

namespace Products.Api.Tests
{
    public class TestsInitializer : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        public Fixture Fixture { get; set; }

        public TestsInitializer()
        {
            var host = new HostBuilder()
                .ConfigureWebJobs(builder=> builder.UseWebJobsStartup(typeof(TestStartup),  new WebJobsBuilderContext(), NullLoggerFactory.Instance))
                //.ConfigureWebJobs(builder => builder.UseWebJobsStartup<TestStartup>())
                .Build();
            
            ServiceProvider = host.Services;
            Fixture = new Fixture();
        }

        public void Dispose()
        {

        }
    }
}