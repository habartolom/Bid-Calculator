using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Interfaces;

public interface IFeeCalculator
{
    decimal Calculate(decimal vehiclePrice, IReadOnlyList<FeeConfigurationDto> feeConfigurations);
    string FeeCode { get; }
}

