namespace Progi.BidCalculator.Infrastructure.Persistence.Entities;

public class FeeType
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }

    public ICollection<FeeConfiguration> Configurations { get; set; } = new List<FeeConfiguration>();
}
