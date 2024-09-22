using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JwtDemo.Application.Features.Identity.ForgotPassword
{
    internal class ForgotPasswordQueryValidator  : AbstractValidator<ForgotPasswordQuery>
    {
        public ForgotPasswordQueryValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
        }
        
    }
}