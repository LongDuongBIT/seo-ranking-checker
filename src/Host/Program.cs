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
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseInfrastructure(app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenAPI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();