using Application;
using Host.Configurations;
using Infrastructure;
using Infrastructure.Logging;
using Infrastructure.OpenAPI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations();

Infrastructure.Logging.Startup.EnsureInitialized(builder.Configuration);
Log.Information("Starting up application...");

builder.RegisterLogging();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseInfrastructure(app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenAPI();
}

app.MapControllers();

app.Run();