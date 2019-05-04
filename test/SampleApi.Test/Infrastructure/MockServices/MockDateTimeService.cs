using System;
using AutoFixture;
using Willow.Infrastructure.Services;
using Xunit;
using Xunit.Abstractions;

namespace Willow.Tests.Infrastructure.MockServices
{
    public class MockDateTimeService : IDateTimeService
    {
        public DateTimeOffset UtcNow { get; set; } = DateTimeOffset.UtcNow;
    }
}