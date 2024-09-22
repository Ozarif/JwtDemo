using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.ResetUserPassword
{
    internal class ResetUserPasswordCommandHandler : ICommandHandler<ResetUserPasswordCommand>
    {
        private readonly IIdentityService _identityService;

        public ResetUserPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.ResetPasswordAsync(request.UserEmail, request.Token, request.NewPassword,cancellationToken);
        }
    }
}