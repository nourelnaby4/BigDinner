using BigDinner.Application.Common.Interfaces.Authentication;
using BigDinner.Application.Common.Interfaces.Date;
using BigDinner.Application.Common.Models;
using BigDinner.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace BigDinner.Service.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IDateTimeProvider _dateProvider;
        public JwtTokenGenerator(
            IOptions<JWT> jwt,
            UserManager<ApplicationUser> userManager,
            IDateTimeProvider dateTimeProvider)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
            _dateProvider = dateTimeProvider;
        }
      

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var claims = await CreateUserClaims(user);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: _dateProvider.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        private async Task<IEnumerable<Claim>> CreateUserClaims(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            return claims;
        }
    }
}
