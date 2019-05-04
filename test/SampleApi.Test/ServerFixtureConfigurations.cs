using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Willow.Tests.Infrastructure;
using Microsoft.Extensions.Configuration;
using SampleApi;
using SampleApi.Database;

namespace Workflow.Tests
{
    public class ServerFixtureConfigurations
    {
        public static readonly ServerFixtureConfiguration SqlServer = new ServerFixtureConfiguration
        {
            EnableTestAuthentication = true,
            StartupType = typeof(Startup),
            MainServicePostAppConfiguration = (configuration, testContext) =>
            {
                if (!string.IsNullOrWhiteSpace(testContext.DatabaseFixture.ConnectionString))
                {
                    var configurationValues = new Dictionary<string, string>
                    {
                        ["ConnectionStrings:SampleDb"] = testContext.DatabaseFixture.ConnectionString,
                    };
                    configuration.AddInMemoryCollection(configurationValues);
                }
            },
            MainServicePostConfigureServices = (services) =>
            {
                //services.ReplaceSingleton<INotificationService, MockNotificationService>();
            }
        };

        public static readonly ServerFixtureConfiguration InMemoryDb = new ServerFixtureConfiguration
        {
            EnableTestAuthentication = true,
            StartupType = typeof(Startup),
            MainServicePostConfigureServices = (services) =>
            {
                var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 8);
                var dbName = $"Test_{uniqueId}";

                //services.ReplaceScoped(GetInMemoryOptions<AssetsContext>(dbName));
                //services.ReplaceSingleton<IBlobService, MockBlobService>();
                services.ReplaceScoped<IDbUpgradeChecker>(_ => new InMemoryDbUpgradeChecker());
            },
            DependencyServices = new List<DependencyServiceConfiguration>
            {
                new DependencyServiceConfiguration
                {
                    Name = "Auth0"
                }
            }
        };

        public static Func<IServiceProvider, DbContextOptions<T>> GetInMemoryOptions<T>(string dbName) where T : DbContext
        {
            return (_) =>
            {
                var builder = new DbContextOptionsBuilder<T>();
                builder.UseInMemoryDatabase(databaseName: dbName);
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                return builder.Options;
            };
        }

        public class InMemoryDbUpgradeChecker : IDbUpgradeChecker
        {
            public void EnsureDatabaseUpToDate(IHostingEnvironment env)
            {
                // Do nothing
            }
        }

    }
}
