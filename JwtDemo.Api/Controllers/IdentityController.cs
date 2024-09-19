using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Application.Features.Identity.LoginUser;
using JwtDemo.Application.Features.Identity.RegisterUser;
using JwtDemo.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ISender _sender;

        public IdentityController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
        {
            var result = await _sender.Send(query);
            if (!result.IsSuccess)
                return Unauthorized(result.Error);

            return Ok(new { Token = result.Value.Token, Expiration = result.Value.Expiration });
        }
    }
}