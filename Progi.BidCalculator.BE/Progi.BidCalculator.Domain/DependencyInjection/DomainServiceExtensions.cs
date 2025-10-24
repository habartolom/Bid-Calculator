using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Services;

namespace Progi.BidCalculator.Domain.DependencyInjection;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<BasicBuyerFeeCalculator>();
        services.AddSingleton<SpecialFeeCalculator>();
        services.AddSingleton<AssociationFeeCalculator>();
        services.AddSingleton<StorageFeeCalculator>();
        services.AddScoped<IBidCalculatorService, BidCalculatorService>();

        return services;
    }
}

