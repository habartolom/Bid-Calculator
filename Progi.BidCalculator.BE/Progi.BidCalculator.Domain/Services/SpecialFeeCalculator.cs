using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Services;

public class SpecialFeeCalculator : IFeeCalculator
{
    public string FeeCode => "SPECIAL_FEE";

    public decimal Calculate(decimal vehiclePrice, IReadOnlyList<FeeConfigurationDto> feeConfigurations)
    {
        if (vehiclePrice <= 0)
            throw new ArgumentException("Vehicle price must be positive", nameof(vehiclePrice));

        var config = feeConfigurations.AsEnumerable().FirstOrDefault();

        if (config == null)
            throw new InvalidOperationException($"No configuration found for {FeeCode}");

        if (!config.Percentage.HasValue)
            throw new InvalidOperationException($"Percentage not configured for {FeeCode}");

        return vehiclePrice * config.Percentage.Value;
    }
}

