using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JwtDemo.Domain.Identity
{
    public class DemoUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}