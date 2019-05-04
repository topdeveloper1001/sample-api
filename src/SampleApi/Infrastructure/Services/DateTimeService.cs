﻿using System;

namespace Willow.Infrastructure.Services
{
    public interface IDateTimeService
    {
        DateTimeOffset UtcNow { get; }
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
