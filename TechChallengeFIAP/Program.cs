using TechChallengeFIAP.Application.Extensions;
using TechChallengeFIAP.Middlewares;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using TechChallengeFIAP.Domain.Validacao;
using TechChallengeFIAP.Application.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<PessoaValidacao>();
builder.Services.AddValidatorsFromAssemblyContaining<JogoValidacao>();

builder.Services.AddControllers();

builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddBasicServices(builder.Configuration)
    .AddProblemDetailsForModelRequestValidation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{ }
