using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Models;

namespace JwtDemo.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage emailMessage);
    }
}