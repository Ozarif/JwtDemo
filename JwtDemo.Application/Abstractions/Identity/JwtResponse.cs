using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtDemo.Application.Abstractions.Identity
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}