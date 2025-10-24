namespace Progi.BidCalculator.Application.DTOs;

public sealed record BidCalculationResponse
{
    public decimal VehicleBasePrice { get; init; }
    public List<AppliedFeeDto> AppliedFees { get; init; } = [];
    public decimal TotalCost { get; init; }
}

public sealed record AppliedFeeDto
{
    public string FeeCode { get; init; } = string.Empty;
    public string FeeName { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public int DisplayOrder { get; init; }
}

