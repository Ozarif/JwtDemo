using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JwtDemo.Application.Features.Identity.LoginUser
{
    internal class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {

       public LoginUserQueryValidator()
        {
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Password).NotEmpty().NotNull();
        }
    }
}