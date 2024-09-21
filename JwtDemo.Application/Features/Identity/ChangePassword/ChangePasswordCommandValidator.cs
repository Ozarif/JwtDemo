using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JwtDemo.Application.Features.Identity.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.CurrentPassword).NotEmpty();
            RuleFor(c => c.NewPassword).NotEmpty();
            RuleFor(c => c.NewPassword).NotEqual(c => c.CurrentPassword).WithMessage("New password must be different from your current password");

        }
    }
}