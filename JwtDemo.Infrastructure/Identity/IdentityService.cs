using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
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

        public async Task<Result<IReadOnlyCollection<UserResponse>>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            var usersResponse = new List<UserResponse>();
            if (users is null)
            {
                return usersResponse;
            }

            foreach (var user in users)
            {
                var userResponse = new UserResponse()
                {
                    UserId = user.Id!,
                    Username = user.UserName!,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList()
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
                return Result.Failure(new Error("DemoUser.ServerError", string.Join(" | ", result.Errors.Select(e => e.Description))));
            // return Result.Failure(result.Errors.Select(e => e.Description).ToList());

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
    }
}