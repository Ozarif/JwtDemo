using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RegisterUserAsync(request.Username, request.Email, request.PhoneNumber, request.Password, request.Roles,cancellationToken);
        }
    }
}