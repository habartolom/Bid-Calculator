using Progi.BidCalculator.Domain.Enums;

namespace Progi.BidCalculator.Domain.Extensions;

public static class VehicleTypeExtensions
{
    public static string ToCode(this VehicleType vehicleType) => vehicleType switch
    {
        VehicleType.Common => "COMMON",
        VehicleType.Luxury => "LUXURY",
        _ => throw new ArgumentException($"Vehicle Type unsupported: {vehicleType}")
    };
}

