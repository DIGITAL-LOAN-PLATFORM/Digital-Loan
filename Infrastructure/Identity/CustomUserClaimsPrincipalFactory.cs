using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            
            // Add custom claims (e.g., FirstName)
            if (!string.IsNullOrEmpty(user.FirstName))
            {
                identity.AddClaim(new Claim("FirstName", user.FirstName));
            }
            if (!string.IsNullOrEmpty(user.LastName))
            {
                identity.AddClaim(new Claim("LastName", user.LastName));
            }

            return identity;
        }
    }
}
