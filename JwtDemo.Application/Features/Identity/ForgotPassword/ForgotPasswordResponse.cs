using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtDemo.Application.Features.Identity.ForgotPassword
{
    public sealed class ForgotPasswordResponse
    {
        public string ResetToken { get; set; } = string.Empty;
    }
}