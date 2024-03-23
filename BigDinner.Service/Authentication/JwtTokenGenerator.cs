using Azure.Core;
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
        private async Task<TokenModel> GenerateToken(string email, string password)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new Exception($"username or password is incorrect");
            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new Exception($"username or password is incorrect");

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
        public async Task<AuthResponse> CreateAuthModel(string email,string password)
        {
           

            var tokenModel = await GenerateToken( email,  password);
            var roles = await _userManager.GetRolesAsync(tokenModel.User);

            return new AuthResponse
            {
                IsAuthenticated = true,
                Token = tokenModel.Token,
                Email = tokenModel.User.Email,
                Username = tokenModel.User.UserName,
                ExpiresOnUtc = tokenModel.ExpiresOnUtc,
                Roles = roles.ToList()
            };
        }
       
    }
}
