using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Messaging;

namespace JwtDemo.Application.Features.Identity.ResetUserPassword;
public sealed record ResetUserPasswordCommand(string UserEmail, string Token, string NewPassword) : ICommand;

