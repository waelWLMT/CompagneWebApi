using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Helpers
{
    public interface IAuthenticationService
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
