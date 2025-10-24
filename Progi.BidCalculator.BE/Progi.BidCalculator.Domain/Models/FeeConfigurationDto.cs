namespace Progi.BidCalculator.Domain.Models;

public sealed class FeeConfigurationDto
{
    public string FeeCode { get; init; } = string.Empty;
    public string FeeName { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
    public string? VehicleTypeCode { get; init; }
    public decimal? Percentage { get; init; }
    public decimal? MinAmountToApply { get; init; }
    public decimal? MaxAmountToApply { get; init; }
    public decimal? FixedAmount { get; init; }
    public decimal? MinVehicleValue { get; init; }
    public decimal? MaxVehicleValue { get; init; }
}
