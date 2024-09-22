using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;

namespace JwtDemo.Application.Features.Identity.CreateRole
{
    public sealed record CreateRoleCommand(string RoleName) : ICommand;

}