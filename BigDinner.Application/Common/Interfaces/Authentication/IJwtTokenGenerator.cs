using BigDinner.Domain.Identities;


namespace BigDinner.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
