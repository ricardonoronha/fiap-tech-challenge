using TechChallengeFIAP.Application.Extensions;
using TechChallengeFIAP.Middlewares;

var builder = WebApplication.CreateBuilder(args);

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
