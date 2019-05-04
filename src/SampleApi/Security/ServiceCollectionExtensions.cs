using SampleApi.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensionsForSecurity
    {
        public static void AddAuth0(this IServiceCollection services, string auth0Domain, string auth0Audience, IHostingEnvironment env)
        {
            string domain = $"https://{auth0Domain}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = auth0Audience;
                options.RequireHttpsMetadata = env.IsDevelopment();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyTypes.IsAdmin, policy => policy.Requirements.Add(new IssuerAndRoleRequirement(domain, Roles.Admin)));
                options.AddPolicy(PolicyTypes.IsAdminOrPortal, policy => policy.Requirements.Add(new IssuerAndRoleRequirement(domain, Roles.Admin, Roles.Portal)));
                options.AddPolicy(PolicyTypes.IsPortal, policy => policy.Requirements.Add(new IssuerAndRoleRequirement(domain, Roles.Portal)));
                options.AddPolicy(PolicyTypes.IsWorkgroup, policy => policy.Requirements.Add(new IssuerAndRoleRequirement(domain, Roles.Workgroup)));
                options.AddPolicy(PolicyTypes.All, policy => policy.Requirements.Add(new IssuerAndRoleRequirement(domain, Roles.AllRoles)));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, IssuerAndRoleHandler>();
        }
    }
}
