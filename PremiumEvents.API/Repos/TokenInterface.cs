using Microsoft.AspNetCore.Identity;

namespace PremiumEvents.API.Repos
{
    public interface TokenInterface
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
