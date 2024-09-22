using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.DeleteRole
{
    internal class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
    {
        private readonly IIdentityService _identityService;

        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.DeleteRoleAsync(request.RoleId,cancellationToken);
        }
    }
}