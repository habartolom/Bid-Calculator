using Progi.BidCalculator.Domain.Enums;
using Progi.BidCalculator.Domain.ValueObjects;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Interfaces;

public interface IBidCalculatorService
{
    BidCalculationResult CalculateTotalCost(decimal VehiclePrice, VehicleType VehicleType, IReadOnlyList<FeeConfigurationDto> FeeConfigurations);
}

