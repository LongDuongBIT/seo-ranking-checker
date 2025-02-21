using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Scraper;

internal static class Startup
{
    internal static IServiceCollection AddScraper(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ScraperSettings>(config.GetSection(nameof(ScraperSettings)));

        return services;
    }
}