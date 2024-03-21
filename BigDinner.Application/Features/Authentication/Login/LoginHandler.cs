using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Authentication.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
