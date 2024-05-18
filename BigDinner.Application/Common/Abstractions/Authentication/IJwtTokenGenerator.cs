using BigDinner.Application.Common.Models;
using BigDinner.Domain.Models.Base;


namespace BigDinner.Application.Common.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<TokenModel> GenerateToken(ApplicationUser user);
        Task<AuthResponse> CreateAuthModel(ApplicationUser user);
    }
}
