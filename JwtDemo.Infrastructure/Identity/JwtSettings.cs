using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtDemo.Infrastructure.Identity
{
    public class JwtSettings
    {
        public const string SettingName = "JwtSettings";
        public string Secret { get; init; } = null!;
        public int ExpirationTimeInMinutes { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}