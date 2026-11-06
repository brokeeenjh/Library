using System.Text;
using Library.Domain.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Auth;

public static class AuthExtension
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration.GetSection("JwtProvider").GetValue<string>("Secret");


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(policy =>
        {
            policy.RequireHttpsMetadata = false;
            policy.SaveToken = true;

            policy.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            policy.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["tasty-cookie"];
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Permissions.Delete, policy =>
            {
                policy.Requirements.Add(new Permission(Permissions.Delete));
                policy.Requirements.Add(new Permission(Permissions.Read));

            });
        });

        return services;
    }
}