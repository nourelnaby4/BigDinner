using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Authentication.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationRequest, RegistrationResponse>
    {
        public Task<RegistrationResponse> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
