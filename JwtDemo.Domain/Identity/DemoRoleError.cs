using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Domain.Identity
{
    public static class DemoRoleError
    {
        public static readonly Error AlreadyExists = new(
                                    "Role.AlreadyExist",
                                    "Role already exists");
        public static Error GetError(IEnumerable<string> errors)
        {
            return new Error("DemoRole.ServerError", string.Join(" | ", errors));
        }
    }
}