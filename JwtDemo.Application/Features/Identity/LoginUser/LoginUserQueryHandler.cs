using System.Security.Principal;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.LoginUser
{
    internal class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, LoginResponse>
    {
        private readonly IIdentityService _identityService;

        public LoginUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<LoginResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginUserAsync(request.Username, request.Password,cancellationToken);
        }
    }
}