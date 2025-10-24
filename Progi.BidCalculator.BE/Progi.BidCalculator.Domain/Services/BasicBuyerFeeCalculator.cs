using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Services;

public class BasicBuyerFeeCalculator : IFeeCalculator
{
    public string FeeCode => "BUYER_FEE";

    public decimal Calculate(decimal vehiclePrice, IReadOnlyList<FeeConfigurationDto> feeConfigurations)
    {
        if (vehiclePrice <= 0)
            throw new ArgumentException("Vehicle price must be positive", nameof(vehiclePrice));

        var config = feeConfigurations.AsEnumerable().FirstOrDefault();

        if (config == null)
            throw new InvalidOperationException($"No configuration found for {FeeCode}");

        if (!config.Percentage.HasValue)
            throw new InvalidOperationException($"Percentage not configured for {FeeCode}");

        var calculatedFee = vehiclePrice * config.Percentage.Value;

        var min = config.MinAmountToApply ?? 0;
        var max = config.MaxAmountToApply ?? decimal.MaxValue;

        if (calculatedFee < min) return min;
        if (calculatedFee > max) return max;

        return calculatedFee;
    }
}

