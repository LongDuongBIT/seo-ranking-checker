using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Middleware;

internal static class Startup
{
    internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services) =>
        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

    internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) =>
        app.UseExceptionHandler();

    internal static IServiceCollection AddRequestLogging(this IServiceCollection services, IConfiguration config) =>
        services
            .AddSingleton<RequestLoggingMiddleware>()
            .AddScoped<ResponseLoggingMiddleware>();

    internal static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app, IConfiguration config) =>
        app
            .UseMiddleware<RequestLoggingMiddleware>()
            .UseMiddleware<ResponseLoggingMiddleware>();
}