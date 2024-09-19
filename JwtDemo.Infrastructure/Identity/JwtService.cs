using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtDemo.Application.Abstractions.Identity;
using JwtDemo.Domain.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemo.Infrastructure.Identity
{
    public class JwtService : IJwtService
    {
     //   private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        public JwtService(IOptions<JwtSettings> jwtSettings)//  ,IConfiguration configuration)
        {
           // _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
        }

        public JwtResponse GenerateJwtToken(DemoUser user, IList<string> roles)
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer, // _configuration["JwtSettings:Issuer"],
                _jwtSettings.Audience, //_configuration["JwtSettings:Audience"],
                claims,
              //  expires: DateTime.Now.AddMinutes(Convert.ToInt64( _configuration["JwtSettings:ExpirationTimeInMinutes"])),
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
                signingCredentials: creds);

            return new JwtResponse(){
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            } ;
        }
    }
}