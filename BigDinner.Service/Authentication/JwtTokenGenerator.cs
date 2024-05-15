using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BigDinner.Service.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsService _userClaimsService;
        private readonly JWT _jwt;
        private readonly IDateTimeProvider _dateProvider;
        public JwtTokenGenerator(
            IOptions<JWT> jwt,
            UserManager<ApplicationUser> userManager,
            IUserClaimsService userClaimsService,
            IDateTimeProvider dateTimeProvider)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
            _userClaimsService = userClaimsService;
            _dateProvider = dateTimeProvider;
        }
        public async Task<TokenModel> GenerateToken(ApplicationUser user)
        {
            var claims = await _userClaimsService.CreateUserClaims(user);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: _dateProvider.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOnUtc = jwtSecurityToken.ValidTo,
                User = user,
            };
        }
        public async Task<AuthResponse> CreateAuthModel(ApplicationUser user)
        {
            var tokenModel = await GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(tokenModel.User);

            return new AuthResponse
            {
                IsAuthenticated = true,
                Token = tokenModel.Token,
                Email = tokenModel.User.Email,
                Username = tokenModel.User.UserName,
                ExpiresOnUtc = tokenModel.ExpiresOnUtc,
                Roles = roles?.ToList()
            };
        }
       
    }
}
