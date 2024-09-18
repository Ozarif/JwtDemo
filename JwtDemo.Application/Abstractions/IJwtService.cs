using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Domain.Identity;

namespace JwtDemo.Application.Abstractions
{
    public interface IJwtService
    {
        JwtResponse GenerateJwtToken(DemoUser user, IList<string> roles);
    }
}