using Infrastructure.Common;
using Infrastructure.Middleware;
using Infrastructure.OpenAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddExceptionMiddleware()
            .AddRequestLogging(config)
            .AddOpenAPI(config)
            .AddServices();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config)
    {
        return app
            .UseExceptionMiddleware()
            .UseRequestLogging(config);
    }
}