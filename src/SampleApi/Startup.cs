using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleApi.Database;
using SampleApi.Domain;
using SampleApi.Services;
using Willow.Infrastructure.Services;

namespace SampleApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _env = env;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiServices(Configuration, _env);

            services.AddMemoryCache();

            services.AddAuth0(Configuration["Auth0:Domain"], Configuration["Auth0:Audience"], _env);
            var connectionString = Configuration.GetConnectionString("SampleDb");
            AddDbContexts(services, connectionString);
            services.AddSingleton<IDbUpgradeChecker, DbUpgradeChecker>();

            services
                .AddHealthChecks()
                .AddDbContextCheck<SampleDbContext>()
                .AddAssemblyVersion();

            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IStudentService, StudentService>();
        }

        private void AddDbContexts(IServiceCollection services, string connectionString)
        {
            void contextOptions(DbContextOptionsBuilder o)
            {
                o.UseSqlServer(connectionString);
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            services.AddDbContext<SampleDbContext>(contextOptions);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbUpgradeChecker dbUpgradeChecker)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseAuthentication();
            app.UseApiServices(Configuration, env);
            dbUpgradeChecker.EnsureDatabaseUpToDate(env);
        }
    }
}
