using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.OpenAPI;

public static class Startup
{
    internal static IServiceCollection AddOpenAPI(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddEndpointsApiExplorer()
            .AddApiVersioning(
                    options =>
                    {
                        options.DefaultApiVersion = new ApiVersion(1, 0);
                        options.ReportApiVersions = true;
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    })
            .AddApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";
            }).Services
            .AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SEO Ranking System APIs v1",
                    Version = "v1",
                });

                o.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "SEO Ranking System APIs v2",
                    Version = "v2",
                });
            });
    }

    public static IApplicationBuilder UseOpenAPI(this WebApplication? app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app?.Services.GetRequiredService<IApiVersionDescriptionProvider>()
                ?? throw new InvalidOperationException("API Version Description Provider is not registered.");

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}