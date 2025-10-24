using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Services;

public class StorageFeeCalculator : IFeeCalculator
{
    public string FeeCode => "STORAGE_FEE";

    public decimal Calculate(decimal vehiclePrice, IReadOnlyList<FeeConfigurationDto> feeConfigurations)
    {
        var config = feeConfigurations.AsEnumerable().FirstOrDefault();

        if (config == null)
            throw new InvalidOperationException($"No configuration found for {FeeCode}");

        return config.FixedAmount ?? throw new InvalidOperationException($"Fixed amount not configured for {FeeCode}");
    }
}

