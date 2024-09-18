using System.Text;
using JwtDemo.Application.Abstractions;
using JwtDemo.Domain.Identity;
using JwtDemo.Infrastructure.Identity;
using JwtDemo.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemo.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
            string connectionString = configuration.GetConnectionString("DefaultConnection") ??
									  throw new ArgumentNullException(nameof(configuration));

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddAuth(configuration);
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services,
                                            IConfiguration configuration)
        {
             services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingName));

            services.AddIdentity<DemoUser, DemoRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

   
        // Configure JWT authentication
        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"]!);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            jwt.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    return c.Response.WriteAsync(c.Exception.ToString());
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "text/plain";
                    return context.Response.WriteAsync("User unauthorized");
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "text/plain";
                    return context.Response.WriteAsync("Access is denied due to insufficient permissions. ");
                },
            };
        });
            
            
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}