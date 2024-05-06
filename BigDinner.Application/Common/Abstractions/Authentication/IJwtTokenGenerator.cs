using BigDinner.Application.Common.Models;
using BigDinner.Domain.Identities;


namespace BigDinner.Application.Common.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<TokenModel> GenerateToken(string email, string password);
        Task<AuthResponse> CreateAuthModel(string email, string password);
    }
}
