using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Features.Identity.LoginUser;
using JwtDemo.Domain.Abstractions;

namespace JwtDemo.Application.Abstractions
{
    public interface IIdentityService
    {
        Task<Result> RegisterUserAsync(string username, string email, string phoneNumber, string password, List<string> roles,CancellationToken cancellationToken=default);
    Task<Result<LoginResponse>> LoginUserAsync(string username, string password,CancellationToken cancellationToken=default);
    Task<bool> IsUserExistsAsync(string username);
    }
}