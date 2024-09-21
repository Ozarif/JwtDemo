using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Application.Features.Identity.ForgotPassword;
using JwtDemo.Application.Features.Identity.GetAllUsers;
using JwtDemo.Application.Features.Identity.LoginUser;
using JwtDemo.Domain.Abstractions;
using JwtDemo.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JwtDemo.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<DemoUser> _userManager;
        private readonly RoleManager<DemoRole> _roleManager;
        private readonly IJwtService _jwtService;

        public IdentityService(UserManager<DemoUser> userManager, RoleManager<DemoRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<Result> ChangePasswordAsync(string UserName, string CurrentPassword, string NewPassword, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
                return Result.Failure(DemoUserError.NotFound);

            IdentityResult result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

            if (result.Succeeded)
                return Result.Success();

            else
                return Result.Failure(DemoUserError.GetError(result.Errors.Select(e => e.Description)));
        }

        public async Task<Result<ForgotPasswordResponse>> ForgotPasswordAsync(string userEmail, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                return Result.Failure<ForgotPasswordResponse>(DemoUserError.NotFound);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Result.Success(new ForgotPasswordResponse() { ResetToken = token });
        }

        public async Task<Result<IReadOnlyCollection<UserResponse>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            var usersResponse = new List<UserResponse>();
            if (users is null)
                return usersResponse;


            foreach (var user in users)
            {
                var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
                var userResponse = new UserResponse()
                {
                    UserId = user.Id!,
                    Username = user.UserName!,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Roles = userRoles
                };
                usersResponse.Add(userResponse);
            }

            return usersResponse;
        }

        public async Task<bool> IsUserExistsAsync(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }

        public async Task<Result<LoginResponse>> LoginUserAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return Result.Failure<LoginResponse>(DemoUserError.InvalidCredentials);

            var roles = await _userManager.GetRolesAsync(user);
            var tokenResponse = _jwtService.GenerateJwtToken(user, roles);

            return Result.Success(new LoginResponse { Token = tokenResponse.Token, Expiration = tokenResponse.Expiration });
        }

        public async Task<Result> RegisterUserAsync(string username, string fullName, string email, string password, List<string> roles, CancellationToken cancellationToken = default)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists != null)
                return Result.Failure(DemoUserError.AlreadyExists);

            var user = new DemoUser
            {
                UserName = username,
                Email = email,
                FullName = fullName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return Result.Failure(DemoUserError.GetError(result.Errors.Select(e => e.Description)));


            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new DemoRole { Name = role });

                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(string userEmail, string token, string newPassword, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                return Result.Failure(DemoUserError.NotFound);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                return Result.Failure(DemoUserError.GetError(result.Errors.Select(e => e.Description)));

            return Result.Success();
        }
    }
}