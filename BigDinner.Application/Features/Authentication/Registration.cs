using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Authentication
{
    public class RegistrationHandler : IRequestHandler<RegistrationRequest, RegistrationResponse>
    {
        public Task<RegistrationResponse> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
    public record RegistrationRequest(Guid Id, string FirstName, string LastName, string Email, string Token) 
        : IRequest<RegistrationResponse>;

    public record RegistrationResponse(Guid Id, string FirstName, string LastName, string Email, string Token);


}
