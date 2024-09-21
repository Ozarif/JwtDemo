using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JwtDemo.Application.Features.Identity.ResetUserPassword
{
    public class ResetUserPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommand>
    {
        public ResetUserPasswordCommandValidator()
        {
            RuleFor(c => c.UserEmail).NotEmpty().EmailAddress().WithMessage("A valid email is required");;
            RuleFor(c => c.Token).NotEmpty();
            RuleFor(c => c.NewPassword).NotEmpty();
        }
    }
}