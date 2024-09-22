using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Email;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Abstractions.Messaging;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Features.Identity.ForgotPassword
{
    internal class ForgotPasswordQueryHandler : IQueryHandler<ForgotPasswordQuery, ForgotPasswordResponse>
    {

        private readonly IIdentityService _identityService;


        public ForgotPasswordQueryHandler(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
        }

        public async Task<Result<ForgotPasswordResponse>> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.ForgotPasswordAsync(request.Email,cancellationToken);
        }
    }
}