using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;

namespace JwtDemo.Application.Features.Identity.ForgotPassword
{
    public sealed record ForgotPasswordQuery(string Email) :IQuery<ForgotPasswordResponse>;
    
}