using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleApi.Security
{
    public class IssuerAndRoleHandler : AuthorizationHandler<IssuerAndRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IssuerAndRoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            var issuerIsValid = context.User.FindFirst(c => c.Issuer == requirement.Issuer) != null;
            var userIsInRequiredRole = context.User.HasClaim(x => x.Type == ClaimTypes.Role && requirement.Roles.Contains(x.Value));
            if (issuerIsValid && userIsInRequiredRole)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
