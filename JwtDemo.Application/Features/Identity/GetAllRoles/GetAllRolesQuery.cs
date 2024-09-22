using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Identity;

namespace JwtDemo.Application.Features.Identity.GetAllRoles
{
    public sealed record GetAllRolesQuery() :IQuery<IReadOnlyCollection<RoleResponse>>;

}