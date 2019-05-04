using DbUp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace SampleApi.Database
{
    public interface IDbUpgradeChecker
    {
        void EnsureDatabaseUpToDate(IHostingEnvironment env);
    }

    public class DbUpgradeChecker : IDbUpgradeChecker
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public DbUpgradeChecker(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public void EnsureDatabaseUpToDate(IHostingEnvironment env)
        {
            var dbUpgradeLogger = _loggerFactory.CreateLogger<DbUpgradeChecker>();
            var connectionString = _configuration.GetConnectionString("Rail");

            try
            {
                var logger = new DbUpgradeLog(dbUpgradeLogger);
                EnsureDatabase.For.SqlDatabase(connectionString, logger);

                var upgradeEngine = DeployChanges.To.SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .WithTransactionPerScript()
                    .LogScriptOutput()
                    .LogTo(logger)
                    .LogToConsole()
                    .Build();

                if (!upgradeEngine.IsUpgradeRequired())
                {
                    dbUpgradeLogger.LogInformation("Database upgrade is not required.");
                    return;
                }

                if (!upgradeEngine.PerformUpgrade().Successful)
                {
                    throw new Exception("DbUp upgrade engine failed to perform upgrade.");
                }
            }
            catch (Exception ex)
            {
                dbUpgradeLogger.LogCritical(ex, "Failed to create or upgrade database.");
                throw;
            }
        }
    }
}
