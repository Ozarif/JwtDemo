using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;

namespace JwtDemo.Application.Features.Identity.ChangePassword
{
    public sealed record ChangePasswordCommand(string UserName, string CurrentPassword, string NewPassword) : ICommand;
  
}