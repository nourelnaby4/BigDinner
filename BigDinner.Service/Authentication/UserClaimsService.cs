using BigDinner.Application.Common.Abstractions.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BigDinner.Service.Authentication
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserClaimsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<Claim>> CreateUserClaims(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(nameof(EnumClaims.Roles), role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(nameof(EnumClaims.UserId), user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            return claims;
        }
        public string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(nameof(EnumClaims.UserId))?.Value;
        }
        public string GetClaimByType(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.FindFirst(claimType)?.Value;
        }
        public Dictionary<string, string> GetAllUserClaims(ClaimsPrincipal claimsPrincipal)
        {
            var dictionaty = new Dictionary<string, string>();
            foreach (var claim in claimsPrincipal.Claims)
            {
                dictionaty.Add(claim.Type, claim.Value);
            }
            return dictionaty;
        }
    }
}
