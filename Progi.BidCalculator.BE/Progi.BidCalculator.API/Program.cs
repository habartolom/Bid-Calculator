using Progi.BidCalculator.API.Middleware;
using Progi.BidCalculator.Application.DependencyInjection;
using Progi.BidCalculator.Domain.DependencyInjection;
using Progi.BidCalculator.Infrastructure.DependencyInjection;
using Progi.BidCalculator.Infrastructure.Extensions;

namespace Progi.BidCalculator.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDomainServices();
        builder.Services.AddApplicationServices();

        var connectionString = Environment.GetEnvironmentVariable("BIDCALC_DB_CONNECTION");
        builder.Services.AddInfrastructure(connectionString);

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(
                        "http://localhost:3000",    // Frontend in Docker
                        "http://localhost:5173",    // Vite dev server (local)
                        "http://localhost:8080",    // Alternative
                        "http://localhost:4200",    // Angular dev
                        "http://frontend:3000"      // Frontend container in Docker network
                      )
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Bid Calculator API",
                Version = "v1",
                Description = "API to calculate the total cost of auction vehicles with all applicable fees",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Progi Development Team"
                }
            });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        var app = builder.Build();

        app.EnsureDatabase();
        app.UseExceptionHandling();
        app.UseSwagger();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bid Calculator API v1"));
        app.UseCors("AllowFrontend");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}