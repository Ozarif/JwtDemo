using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.GetAllUsers
{
    public class GetAllUsersQuery  : IQuery<IReadOnlyCollection<UserResponse>>
    {
    }
}