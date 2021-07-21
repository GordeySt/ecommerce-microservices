using Identity.Domain.Entities;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            var claims = userClaims.Claims.ToList();
            claims = claims
                .Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, sub));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            if (_userManager.SupportsUserRole)
            {
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));

                    if (_roleManager.SupportsRoleClaims)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);

                        if (role is not null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user is not null;
        }
    }
}
