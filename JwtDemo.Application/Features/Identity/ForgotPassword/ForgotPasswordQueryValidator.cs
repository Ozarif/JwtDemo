using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace JwtDemo.Application.Features.Identity.ForgotPassword
{
    public class ForgotPasswordQueryValidator  : AbstractValidator<ForgotPasswordQuery>
    {
        public ForgotPasswordQueryValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
        }
        
    }
}