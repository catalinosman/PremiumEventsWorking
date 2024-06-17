using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using PremiumEvents.API.Models; 

namespace PremiumEvents.API.Authorization
{
    public class ModifyServiceAuthorizationHandler : AuthorizationHandler<ModifyServiceRequirement, Service>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ModifyServiceRequirement requirement, Service service)
        {
            if (context.User.IsInRole("Master") ||
                context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier && c.Value == service.CreatedBy))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ModifyServiceRequirement : IAuthorizationRequirement { }
}