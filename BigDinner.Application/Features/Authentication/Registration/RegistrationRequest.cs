using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Authentication.Registration
{
    public record RegistrationRequest(Guid Id,string FirstName,string LastName,string Email,string Token);
}
