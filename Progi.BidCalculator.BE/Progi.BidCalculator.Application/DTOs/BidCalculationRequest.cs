using Progi.BidCalculator.Domain.Enums;

namespace Progi.BidCalculator.Application.DTOs;

public sealed record BidCalculationRequest
{
    public decimal VehicleBasePrice { get; init; }
    public VehicleType VehicleType { get; init; }
}

