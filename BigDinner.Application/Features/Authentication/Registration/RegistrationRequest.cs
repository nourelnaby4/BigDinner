using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Authentication.Registration
{

    public record RegistrationRequest(Guid Id, string Username, string Email,string Password, string Token) : IRequest<AuthResponse>;


}
