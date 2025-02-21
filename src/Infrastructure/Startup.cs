using Application.Common.Interfaces;
using Infrastructure.Caching;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Infrastructure.Cors;
using Infrastructure.Middleware;
using Infrastructure.OpenAPI;
using Infrastructure.Scraper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddOpenAPI(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddRequestLogging(config)
            .AddScraper(config)
            .AddServicesAuto()
            .AddServices();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config)
    {
        return app
            .UseHttpsRedirection()
            .UseExceptionMiddleware()
            .UseCorsPolicy()
            .UseRequestLogging(config);
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddKeyedScoped<ISearchEngineScraper, GoogleSearchEngineScrapeStrategy>("Google");
        services.AddKeyedScoped<ISearchEngineScraper, BingSearchEngineScrapeStrategy>("Bing");

        return services;
    }
}