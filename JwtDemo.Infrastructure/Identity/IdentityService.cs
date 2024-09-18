using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions;
using JwtDemo.Application.Features.Identity.LoginUser;
using JwtDemo.Domain.Abstractions;
using JwtDemo.Domain.Identity;
using Microsoft.AspNetCore.Identity;

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

            return Result.Success(new LoginResponse { Token = tokenResponse.Token,Expiration= tokenResponse.Expiration });
        }

        public async Task<Result> RegisterUserAsync(string username, string email, string phoneNumber, string password, List<string> roles, CancellationToken cancellationToken = default)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists != null)
                return Result.Failure(DemoUserError.AlreadyExists);

            var user = new DemoUser
            {
                UserName = username,
                Email = email,
                PhoneNumber = phoneNumber
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return Result.Failure(DemoUserError.ServerError);
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