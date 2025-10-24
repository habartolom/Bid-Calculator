namespace Progi.BidCalculator.Infrastructure.Persistence.Entities;

public class VehicleType
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public ICollection<FeeConfiguration> FeeConfigurations { get; set; } = new List<FeeConfiguration>();
}

