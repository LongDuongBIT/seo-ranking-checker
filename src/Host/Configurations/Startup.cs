﻿namespace Host.Configurations;

internal static class Startup
{
    internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var env = builder.Environment;
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/logger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/cache.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/cache.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/cors.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/cors.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/scraper.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/scraper.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

        return builder;
    }
}