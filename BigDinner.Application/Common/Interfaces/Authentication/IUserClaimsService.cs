using BigDinner.Domain.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Common.Interfaces.Authentication
{
    public interface IUserClaimsService
    {
        Task<IEnumerable<Claim>> CreateUserClaims(ApplicationUser user);
        string GetUserId(ClaimsPrincipal claimsPrincipal);
        Dictionary<string, string> GetAllUserClaims(ClaimsPrincipal claimsPrincipal);
    }
}
