namespace Progi.BidCalculator.Infrastructure.Persistence.Entities;

public class FeeConfiguration
{
    public int Id { get; set; }
    public int FeeTypeId { get; set; }
    public int? VehicleTypeId { get; set; }
    
    public decimal? Percentage { get; set; }
    public decimal? MinAmountToApply { get; set; }
    public decimal? MaxAmountToApply { get; set; }
    public decimal? FixedAmount { get; set; }
    
    public decimal? MinVehicleValue { get; set; }
    public decimal? MaxVehicleValue { get; set; }
    
    public string? Notes { get; set; }

    // Navegación
    public FeeType FeeType { get; set; } = null!;
    public VehicleType? VehicleType { get; set; }
}
