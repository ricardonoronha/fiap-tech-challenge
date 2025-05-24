using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Application.Interfaces;
using TechChallengeFIAP.Application.Services;
using TechChallengeFIAP.Application.Settings;
using TechChallengeFIAP.Data;
using TechChallengeFIAP.Data.Repositorios;
using TechChallengeFIAP.Domain.Interfaces;


namespace TechChallengeFIAP.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<ISenhaHasher, SenhaHasher>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IPessoaRepositorio, PessoaRepositorio>()
            .AddScoped<IEventStoreRepository, EventStoreRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
        
    }

    public static IServiceCollection AddBasicServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        
        configuration
            .GetSection("JwtSettings")
            .Bind(jwtSettings);


        services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            })
            .AddStandardOptions<JwtSettings>()
            .AddJwtAuthentication(configuration)
            .AddAppServices();

        return services;
    }

    public static IServiceCollection AddStandardOptions<T>(this IServiceCollection services)
        where T : class
    {
        string sectionTypeName = typeof(T).Name;

        services
            .AddOptions<T>()
            .BindConfiguration(sectionTypeName)
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
