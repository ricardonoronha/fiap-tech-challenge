using Datadog.Trace;
using Datadog.Trace.Configuration;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using TechChallengeFIAP.Application.Extensions;
using TechChallengeFIAP.Domain.Validacao;
using TechChallengeFIAP.Middlewares;


Env.Load();

var settings = TracerSettings.FromDefaultSources();
Tracer.Configure(settings);

var defaultLogger = new LoggerConfiguration()
   .MinimumLevel.Information()
   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
   .Enrich.FromLogContext()
   .Enrich.WithEnvironmentName()
   .Enrich.WithMachineName()
   .Enrich.WithProcessId()
   .Enrich.WithThreadId()
   .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter(renderMessage: true))
   .CreateLogger();

Log.Logger = defaultLogger;

var builder = WebApplication.CreateBuilder(args);

// Usa o Serilog
builder.Host.UseSerilog();

// Configuração inicial do Serilog

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<PessoaValidacao>();
builder.Services.AddValidatorsFromAssemblyContaining<JogoValidacao>();

builder.Services.AddControllers();

builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer {SeuToken}' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    })
    .AddBasicServices(builder.Configuration)
    .AddProblemDetailsForModelRequestValidation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{ }
