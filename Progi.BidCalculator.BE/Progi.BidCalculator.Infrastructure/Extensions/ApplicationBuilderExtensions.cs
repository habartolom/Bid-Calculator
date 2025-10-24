using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Domain.Interfaces;

namespace Progi.BidCalculator.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        databaseInitializer.Initialize();
    }
}

