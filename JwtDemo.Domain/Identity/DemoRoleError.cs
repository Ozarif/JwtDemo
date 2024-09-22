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

        //Cannot delete role '{roleName}' because it is assigned to one or more users.

        public static readonly Error RoleInUse = new(
                            "Role.RoleInUse",
                            "Role is used by users");

        public static readonly Error NotFound = new(
                                                "Role.NotFound",
                                                "Role not found");
        public static Error GetError(IEnumerable<string> errors)
        {
            return new Error("DemoRole.ServerError", string.Join(" | ", errors));
        }
    }
}