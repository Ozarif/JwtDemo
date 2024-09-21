using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Features.Identity.ForgotPassword;
using JwtDemo.Application.Features.Identity.GetAllUsers;
using JwtDemo.Application.Features.Identity.LoginUser;
using JwtDemo.Domain.Abstractions;
using JwtDemo.Domain.Identity;

namespace JwtDemo.Application.Abstractions.Identity
{
    public interface IIdentityService
    {
        Task<Result<IReadOnlyCollection<UserResponse>>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<Result> RegisterUserAsync(string username, string email, string phoneNumber, string password, List<string> roles, CancellationToken cancellationToken = default);
        Task<Result<LoginResponse>> LoginUserAsync(string username, string password, CancellationToken cancellationToken = default);
        Task<bool> IsUserExistsAsync(string username);
        Task<Result<ForgotPasswordResponse>> ForgotPasswordAsync(string userEmail, CancellationToken cancellationToken = default);
        Task<Result> ResetPasswordAsync(string userEmail, string token, string newPassword, CancellationToken cancellationToken = default);
        Task<Result> ChangePasswordAsync(string UserName, string CurrentPassword, string NewPassword, CancellationToken cancellationToken = default);
    }
}