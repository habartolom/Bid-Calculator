using Microsoft.EntityFrameworkCore;
using Progi.BidCalculator.Infrastructure.Persistence.Entities;

namespace Progi.BidCalculator.Infrastructure.Persistence;

public class BidCalculatorDbContext : DbContext
{
    public BidCalculatorDbContext(DbContextOptions<BidCalculatorDbContext> options) : base(options)
    {
    }

    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<FeeType> FeeTypes => Set<FeeType>();
    public DbSet<FeeConfiguration> FeeConfigurations => Set<FeeConfiguration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.ToTable("vehicle_types");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<FeeType>(entity =>
        {
            entity.ToTable("fee_types");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<FeeConfiguration>(entity =>
        {
            entity.ToTable("fee_configurations");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FeeTypeId).HasColumnName("fee_type_id");
            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");
            
            entity.Property(e => e.Percentage).HasColumnName("percentage").HasColumnType("numeric(10,6)");
            entity.Property(e => e.MinAmountToApply).HasColumnName("min_amount_to_apply").HasColumnType("numeric(18,2)");
            entity.Property(e => e.MaxAmountToApply).HasColumnName("max_amount_to_apply").HasColumnType("numeric(18,2)");
            entity.Property(e => e.FixedAmount).HasColumnName("fixed_amount").HasColumnType("numeric(18,2)");
            
            entity.Property(e => e.MinVehicleValue).HasColumnName("min_vehicle_value").HasColumnType("numeric(18,2)");
            entity.Property(e => e.MaxVehicleValue).HasColumnName("max_vehicle_value").HasColumnType("numeric(18,2)");
            
            entity.Property(e => e.Notes).HasColumnName("notes");
            
            entity.HasOne(e => e.FeeType)
                .WithMany(ft => ft.Configurations)
                .HasForeignKey(e => e.FeeTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.VehicleType)
                .WithMany(vt => vt.FeeConfigurations)
                .HasForeignKey(e => e.VehicleTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        SeedVehicleTypes(modelBuilder);
        SeedFeeTypes(modelBuilder);
        SeedFeeConfigurations(modelBuilder);
    }

    private static void SeedVehicleTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleType>().HasData(
            new VehicleType { Id = 1, Code = "COMMON", Name = "Common Vehicle", Description = "Standard common vehicle", IsActive = true },
            new VehicleType { Id = 2, Code = "LUXURY", Name = "Luxury Vehicle", Description = "Premium luxury vehicle", IsActive = true }
        );
    }

    private static void SeedFeeTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FeeType>().HasData(
            new FeeType 
            { 
                Id = 1, 
                Code = "BUYER_FEE", 
                Name = "Basic Buyer Fee", 
                Description = "Basic buyer fee (10% with limits based on vehicle type)",
                IsActive = true,
                DisplayOrder = 1
            },
            new FeeType 
            { 
                Id = 2, 
                Code = "SPECIAL_FEE", 
                Name = "Special Fee", 
                Description = "Special seller fee (percentage based on vehicle type)",
                IsActive = true,
                DisplayOrder = 2
            },
            new FeeType 
            { 
                Id = 3, 
                Code = "ASSOCIATION_FEE", 
                Name = "Association Fee", 
                Description = "Association fee based on price ranges (vehicle type independent)",
                IsActive = true,
                DisplayOrder = 3
            },
            new FeeType 
            { 
                Id = 4, 
                Code = "STORAGE_FEE", 
                Name = "Storage Fee", 
                Description = "Fixed storage fee (vehicle type independent)",
                IsActive = true,
                DisplayOrder = 4
            }
        );
    }

    private static void SeedFeeConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FeeConfiguration>().HasData(
            new FeeConfiguration 
            { 
                Id = 1, 
                FeeTypeId = 1, 
                VehicleTypeId = 1, // COMMON
                Percentage = 0.10m, 
                MinAmountToApply = 10m, 
                MaxAmountToApply = 50m,
                Notes = "10% with minimum $10 and maximum $50 for Common vehicles"
            },
            new FeeConfiguration 
            { 
                Id = 2, 
                FeeTypeId = 1, 
                VehicleTypeId = 2, // LUXURY
                Percentage = 0.10m, 
                MinAmountToApply = 25m, 
                MaxAmountToApply = 200m,
                Notes = "10% with minimum $25 and maximum $200 for Luxury vehicles"
            },
            new FeeConfiguration 
            { 
                Id = 3, 
                FeeTypeId = 2, 
                VehicleTypeId = 1, // COMMON
                Percentage = 0.02m,
                Notes = "2% for Common vehicles"
            },
            new FeeConfiguration 
            { 
                Id = 4, 
                FeeTypeId = 2, 
                VehicleTypeId = 2, // LUXURY
                Percentage = 0.04m,
                Notes = "4% for Luxury vehicles"
            },
            new FeeConfiguration 
            { 
                Id = 5, 
                FeeTypeId = 4, 
                VehicleTypeId = null, // Applies to all
                FixedAmount = 100m,
                Notes = "Fixed fee of $100 for all vehicles"
            },
            new FeeConfiguration 
            { 
                Id = 6, 
                FeeTypeId = 3, 
                VehicleTypeId = null,
                MinVehicleValue = 0m, 
                MaxVehicleValue = 500m, 
                FixedAmount = 5m, 
                Notes = "Greater than $0 up to and including $500" 
            },
            new FeeConfiguration 
            { 
                Id = 7, 
                FeeTypeId = 3, 
                VehicleTypeId = null,
                MinVehicleValue = 500m, 
                MaxVehicleValue = 1000m, 
                FixedAmount = 10m, 
                Notes = "Greater than $500 up to and including $1000" 
            },
            new FeeConfiguration 
            { 
                Id = 8, 
                FeeTypeId = 3, 
                VehicleTypeId = null,
                MinVehicleValue = 1000m, 
                MaxVehicleValue = 3000m, 
                FixedAmount = 15m, 
                Notes = "Greater than $1000 up to and including $3000" 
            },
            new FeeConfiguration 
            { 
                Id = 9, 
                FeeTypeId = 3, 
                VehicleTypeId = null,
                MinVehicleValue = 3000m, 
                MaxVehicleValue = null, 
                FixedAmount = 20m, 
                Notes = "Greater than $3000 (no upper limit)" 
            }
        );
    }
}
