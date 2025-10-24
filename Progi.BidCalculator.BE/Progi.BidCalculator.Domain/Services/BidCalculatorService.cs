using Progi.BidCalculator.Domain.Enums;
using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.ValueObjects;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Services;

public class BidCalculatorService(
    BasicBuyerFeeCalculator basicBuyerFeeCalculator,
    SpecialFeeCalculator specialFeeCalculator,
    AssociationFeeCalculator associationFeeCalculator,
    StorageFeeCalculator storageFeeCalculator
) : IBidCalculatorService
{
    private readonly IReadOnlyDictionary<string, IFeeCalculator> _calculators = new Dictionary<string, IFeeCalculator>
    {
        [basicBuyerFeeCalculator.FeeCode] = basicBuyerFeeCalculator ?? throw new ArgumentNullException(nameof(basicBuyerFeeCalculator)),
        [specialFeeCalculator.FeeCode] = specialFeeCalculator ?? throw new ArgumentNullException(nameof(specialFeeCalculator)),
        [associationFeeCalculator.FeeCode] = associationFeeCalculator ?? throw new ArgumentNullException(nameof(associationFeeCalculator)),
        [storageFeeCalculator.FeeCode] = storageFeeCalculator ?? throw new ArgumentNullException(nameof(storageFeeCalculator))
    };

    public BidCalculationResult CalculateTotalCost(decimal vehiclePrice, VehicleType vehicleType, IReadOnlyList<FeeConfigurationDto> feeConfigurations)
    {
        if (vehiclePrice <= 0)
            throw new ArgumentException("Vehicle price must be greater than zero", nameof(vehiclePrice));

        var appliedFees = new List<AppliedFee>();

        var groupedConfigs = feeConfigurations
            .GroupBy(c => new { c.FeeCode, c.FeeName, c.DisplayOrder })
            .OrderBy(g => g.Key.DisplayOrder);

        foreach (var group in groupedConfigs)
        {
            if (!_calculators.TryGetValue(group.Key.FeeCode, out var calculator))
                continue;

            var feeConfigs = group.ToList();
            var amount = calculator.Calculate(vehiclePrice, feeConfigs);
            
            var appliedFee = new AppliedFee(
                FeeCode: group.Key.FeeCode, 
                FeeName: group.Key.FeeName, 
                Amount: amount, 
                DisplayOrder: group.Key.DisplayOrder
            );
            
            appliedFees.Add(appliedFee);
        }

        return new BidCalculationResult(vehiclePrice, appliedFees);
    }
}

