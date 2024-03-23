using BigDinner.Application.Features.Authentication;
using BigDinner.Domain.Identities;


namespace BigDinner.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<TokenModel> GenerateToken(string email, string password);
        Task<AuthResponse> CreateAuthModel(string email, string password);
    }
}
