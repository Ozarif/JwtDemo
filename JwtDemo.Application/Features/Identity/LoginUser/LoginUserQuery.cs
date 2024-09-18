using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;

namespace JwtDemo.Application.Features.Identity.LoginUser
{
    public class LoginUserQuery :IQuery<LoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}