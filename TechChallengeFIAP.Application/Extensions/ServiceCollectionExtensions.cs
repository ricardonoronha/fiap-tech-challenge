using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Application.Settings;
using TechChallengeFIAP.Data.Repositorios;


namespace TechChallengeFIAP.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBasicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            })
            .AddStandardOptions<JwtSettings>()
            .AddJwtAuthentication(configuration);

        return services;
    }

    public static IServiceCollection AddStandardOptions<T>(this IServiceCollection services)
        where T : class
    {
        services
            .AddOptions<T>(nameof(T))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication()
            .AddJwtBearer(jwtOptions =>
            {
                var jwtSettings = configuration
                        .GetSection(nameof(JwtSettings))
                        .Get<JwtSettings>()!;

                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.FromMinutes(jwtSettings.TokenTimeToleranceInMinutes),

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });


        return services;
    }
}
