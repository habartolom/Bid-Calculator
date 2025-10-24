using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Infrastructure.Persistence;
using Progi.BidCalculator.Infrastructure.Repositories;

namespace Progi.BidCalculator.Infrastructure.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string not configured. Define the BIDCALC_DB_CONNECTION environment variable.");
        }

        services.AddDbContext<BidCalculatorDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IFeesConfigRepository, FeesConfigRepository>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        return services;
    }
}


