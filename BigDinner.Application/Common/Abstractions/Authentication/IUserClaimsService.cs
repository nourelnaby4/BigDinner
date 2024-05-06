using BigDinner.Domain.Identities;
using System.Security.Claims;


namespace BigDinner.Application.Common.Abstractions.Authentication;

public interface IUserClaimsService
{
    Task<IEnumerable<Claim>> CreateUserClaims(ApplicationUser user);
    string GetUserId(ClaimsPrincipal claimsPrincipal);
    Dictionary<string, string> GetAllUserClaims(ClaimsPrincipal claimsPrincipal);
}
