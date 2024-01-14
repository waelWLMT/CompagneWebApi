using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class AuthenticateResponse
    {
        public AuthenticatedUserDto User { get; set; }
        public string Token { get; set; }        

        public AuthenticateResponse(AuthenticatedUserDto user, string token)
        {            
            this.User = user;
            this.Token = token;
        }
    }
}
