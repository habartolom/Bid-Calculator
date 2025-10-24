using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Services;

public class AssociationFeeCalculator : IFeeCalculator
{
    public string FeeCode => "ASSOCIATION_FEE";

    public decimal Calculate(decimal vehiclePrice, IReadOnlyList<FeeConfigurationDto> feeConfigurations)
    {
        if (vehiclePrice <= 0)
            throw new ArgumentException("Vehicle price must be positive", nameof(vehiclePrice));

        var tiers = feeConfigurations
            .Where(c => c is { MinVehicleValue: not null, FixedAmount: not null })
            .OrderBy(c => c.MinVehicleValue)
            .ToList();

        if (tiers.Count == 0)
            throw new InvalidOperationException($"No tiers configured for {FeeCode}");

        var matchedTier = tiers.FirstOrDefault(tier =>
            vehiclePrice > tier.MinVehicleValue!.Value &&
            (!tier.MaxVehicleValue.HasValue || vehiclePrice <= tier.MaxVehicleValue.Value)
        );
        
        return matchedTier == null 
            ? throw new ArgumentException($"Vehicle price ${vehiclePrice} is out of configured range")
            : matchedTier.FixedAmount!.Value;
    }
}

