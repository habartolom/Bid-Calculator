namespace Progi.BidCalculator.Domain.ValueObjects;

public sealed record BidCalculationResult(
    decimal VehicleBasePrice,
    IReadOnlyList<AppliedFee> AppliedFees
)
{
    public readonly decimal TotalCost = VehicleBasePrice + AppliedFees.Sum(f => f.Amount);
}

