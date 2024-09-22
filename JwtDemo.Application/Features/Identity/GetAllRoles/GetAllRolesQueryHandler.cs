using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.GetAllRoles
{
    internal class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, IReadOnlyCollection<RoleResponse>>
    {
         private readonly IIdentityService _identityService;

        public GetAllRolesQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<IReadOnlyCollection<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetAllRolesAsync();
        }
    }
}