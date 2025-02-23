using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Infrastructure.Scraper;

internal static class Startup
{
    internal static IServiceCollection AddScraper(this IServiceCollection services, IConfiguration config)
    {
        var scraperConfig = config.GetSection(nameof(ScraperSettings));
        services.Configure<ScraperSettings>(scraperConfig);
        var settings = scraperConfig.Get<ScraperSettings>() ?? throw new InvalidOperationException("ScraperSettings is not configured");

        services.AddScoped(f =>
        {
            return new Lazy<IWebDriver>(() =>
            {
                var options = new ChromeOptions();
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                return new RemoteWebDriver(new Uri(settings.SeleniumGridUrl!), options);
            });
        });

        return services;
    }
}