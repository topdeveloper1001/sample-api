using AutoFixture;
using Xunit;
using Xunit.Abstractions;

namespace Willow.Tests.Infrastructure
{
    [Collection(TestCollectionDefinition.Name)]
    public class BaseDbTest : BaseTest
    {
        public BaseDbTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override TestContext TestContext => new TestContext(Output, DatabaseFixture.Instance);
        protected static Fixture StaticFixture = new Fixture();
    }
}
