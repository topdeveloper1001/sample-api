using System;
using Microsoft.AspNetCore.Authorization;

namespace SampleApi.Security
{
    public class IssuerAndRoleRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string[] Roles { get; }

        public IssuerAndRoleRequirement(string issuer, params string[] roles)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}
