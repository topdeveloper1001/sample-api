using Xunit;
using Xunit.Abstractions;

namespace Willow.Tests.Infrastructure
{
    [CollectionDefinition(TestCollectionDefinition_InMemory.Name)]
    public class BaseInMemoryTest : BaseTest
    {
        public BaseInMemoryTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override TestContext TestContext => new TestContext(Output, null);
    }
}
