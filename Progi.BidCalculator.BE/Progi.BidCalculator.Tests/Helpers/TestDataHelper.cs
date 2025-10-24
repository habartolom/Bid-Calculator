using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Tests.Helpers;

public static class TestDataHelper
{
    public static IReadOnlyList<FeeConfigurationDto> GetAllFeeConfigurations() => new List<FeeConfigurationDto>
    {
        new ()
        {
            FeeCode = "BUYER_FEE",
            FeeName = "Basic Buyer Fee",
            DisplayOrder = 1,
            VehicleTypeCode = "COMMON",
            Percentage = 0.10m,
            MinAmountToApply = 10m,
            MaxAmountToApply = 50m
        },
        new ()
        {
            FeeCode = "BUYER_FEE",
            FeeName = "Basic Buyer Fee",
            DisplayOrder = 1,
            VehicleTypeCode = "LUXURY",
            Percentage = 0.10m,
            MinAmountToApply = 25m,
            MaxAmountToApply = 200m
        },
        new ()
        {
            FeeCode = "SPECIAL_FEE",
            FeeName = "Special Fee",
            DisplayOrder = 2,
            VehicleTypeCode = "COMMON",
            Percentage = 0.02m
        },
        new ()
        {
            FeeCode = "SPECIAL_FEE",
            FeeName = "Special Fee",
            DisplayOrder = 2,
            VehicleTypeCode = "LUXURY",
            Percentage = 0.04m
        },
        new ()
        {
            FeeCode = "STORAGE_FEE",
            FeeName = "Storage Fee",
            DisplayOrder = 4,
            VehicleTypeCode = null,
            FixedAmount = 100m
        },
        new ()
        {
            FeeCode = "ASSOCIATION_FEE",
            FeeName = "Association Fee",
            DisplayOrder = 3,
            VehicleTypeCode = null,
            MinVehicleValue = 0m,
            MaxVehicleValue = 500m,
            FixedAmount = 5m
        },
        new ()
        {
            FeeCode = "ASSOCIATION_FEE",
            FeeName = "Association Fee",
            DisplayOrder = 3,
            VehicleTypeCode = null,
            MinVehicleValue = 500m,
            MaxVehicleValue = 1000m,
            FixedAmount = 10m
        },
        new ()
        {
            FeeCode = "ASSOCIATION_FEE",
            FeeName = "Association Fee",
            DisplayOrder = 3,
            VehicleTypeCode = null,
            MinVehicleValue = 1000m,
            MaxVehicleValue = 3000m,
            FixedAmount = 15m
        },
        new ()
        {
            FeeCode = "ASSOCIATION_FEE",
            FeeName = "Association Fee",
            DisplayOrder = 3,
            VehicleTypeCode = null,
            MinVehicleValue = 3000m,
            MaxVehicleValue = null,
            FixedAmount = 20m
        }
    };
}

