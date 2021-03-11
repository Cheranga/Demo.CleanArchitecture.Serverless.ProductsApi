using Xunit;

namespace Products.Api.Tests
{
    [CollectionDefinition(Name)]
    public class IntegrationTestsCollection : ICollectionFixture<TestsInitializer>
    {
        public const string Name = nameof(IntegrationTestsCollection);
    }
}