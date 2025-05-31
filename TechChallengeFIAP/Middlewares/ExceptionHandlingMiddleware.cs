using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFIAP.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext context)
    {

        try
        {
            await _next(context); // Continua o pipeline
        }
        catch (Exception ex)
        {
            string traceId = context.TraceIdentifier.Split(':')[0];

            _logger.LogError(ex, "Ocorreu uma exceção não tratada | TraceId: {TraceId}", traceId);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Title = "Erro inesperado",
                Status = StatusCodes.Status500InternalServerError,
                Detail = _env.IsDevelopment() ? ex.ToString() : "Ocorreu um erro interno no servidor",
                Instance = context.Request.Path,
                Extensions =
                {
                    {  "TraceId", traceId }
                }
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}