using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Infrastructure.Logging;

public static class Startup
{
    public static void EnsureInitialized(IConfiguration configuration)
    {
        if (Log.Logger is not Serilog.Core.Logger)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Async(a => a.Console())
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }
    }

    public static void RegisterLogging(this WebApplicationBuilder builder)
    {
        _ = builder.Host.UseSerilog((_, sp, serilogConfig) =>
        {
            serilogConfig.WriteTo.Async(x => x.Console()).ReadFrom.Configuration(builder.Configuration);
        });
    }
};