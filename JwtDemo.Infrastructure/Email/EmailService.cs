using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Email;
using JwtDemo.Application.Models;

namespace JwtDemo.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(EmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }
    }
}