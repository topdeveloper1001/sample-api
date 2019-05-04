using Xunit;

namespace Willow.Tests.Infrastructure
{
    [CollectionDefinition(Name)]
    public partial class TestCollectionDefinition
    {
        public const string Name = "Global collection";
    }
}