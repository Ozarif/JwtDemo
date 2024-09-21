using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.GetAllUsers
{
    internal class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IReadOnlyCollection<UserResponse>>
    {
        private readonly IIdentityService _identityService;

        public GetAllUsersQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<IReadOnlyCollection<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetAllUsersAsync(cancellationToken);
        }
    }
}