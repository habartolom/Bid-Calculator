using FluentAssertions;
using Progi.BidCalculator.Domain.Enums;
using Progi.BidCalculator.Domain.Extensions;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Domain.Services;
using Progi.BidCalculator.Tests.Helpers;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Domain.Services;

public class BidCalculatorServiceTests
{
    private readonly BidCalculatorService _service;

    public BidCalculatorServiceTests()
    {
        var basicBuyerFeeCalculator = new BasicBuyerFeeCalculator();
        var specialFeeCalculator = new SpecialFeeCalculator();
        var associationFeeCalculator = new AssociationFeeCalculator();
        var storageFeeCalculator = new StorageFeeCalculator();
        
        _service = new BidCalculatorService(
            basicBuyerFeeCalculator,
            specialFeeCalculator,
            associationFeeCalculator,
            storageFeeCalculator
        );
    }

    private static IReadOnlyList<FeeConfigurationDto> GetFilteredConfig(VehicleType vehicleType)
    {
        var vehicleTypeCode = vehicleType.ToCode();
        return TestDataHelper.GetAllFeeConfigurations()
            .Where(c => c.VehicleTypeCode == null || c.VehicleTypeCode == vehicleTypeCode)
            .ToList();
    }

    [Fact]
    public void CalculateTotalCost_TestCase1_Common398_ReturnsCorrectValues()
    {
        // Arrange
        var price = 398.00m;
        var vehicleType = VehicleType.Common;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(398.00m);
        result.AppliedFees.Should().HaveCount(4);
        
        var buyerFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE");
        buyerFee.Should().NotBeNull();
        buyerFee!.Amount.Should().Be(39.80m);
        buyerFee.FeeName.Should().Be("Basic Buyer Fee");
        
        var specialFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE");
        specialFee!.Amount.Should().Be(7.96m);
        
        var associationFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE");
        associationFee!.Amount.Should().Be(5.00m);
        
        var storageFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE");
        storageFee!.Amount.Should().Be(100.00m);
        
        result.TotalCost.Should().Be(550.76m);
    }

    [Fact]
    public void CalculateTotalCost_TestCase2_Common501_ReturnsCorrectValues()
    {
        // Arrange
        var price = 501.00m;
        var vehicleType = VehicleType.Common;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(501.00m);
        result.AppliedFees.Should().HaveCount(4);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE")!.Amount.Should().Be(50.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE")!.Amount.Should().Be(10.02m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE")!.Amount.Should().Be(10.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE")!.Amount.Should().Be(100.00m);
        result.TotalCost.Should().Be(671.02m);
    }

    [Fact]
    public void CalculateTotalCost_TestCase3_Common57_ReturnsCorrectValues()
    {
        // Arrange
        var price = 57.00m;
        var vehicleType = VehicleType.Common;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(57.00m);
        result.AppliedFees.Should().HaveCount(4);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE")!.Amount.Should().Be(10.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE")!.Amount.Should().Be(1.14m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE")!.Amount.Should().Be(5.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE")!.Amount.Should().Be(100.00m);
        result.TotalCost.Should().Be(173.14m);
    }

    [Fact]
    public void CalculateTotalCost_TestCase4_Luxury1800_ReturnsCorrectValues()
    {
        // Arrange
        var price = 1800.00m;
        var vehicleType = VehicleType.Luxury;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(1800.00m);
        result.AppliedFees.Should().HaveCount(4);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE")!.Amount.Should().Be(180.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE")!.Amount.Should().Be(72.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE")!.Amount.Should().Be(15.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE")!.Amount.Should().Be(100.00m);
        result.TotalCost.Should().Be(2167.00m);
    }

    [Fact]
    public void CalculateTotalCost_TestCase5_Common1100_ReturnsCorrectValues()
    {
        // Arrange
        var price = 1100.00m;
        var vehicleType = VehicleType.Common;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(1100.00m);
        result.AppliedFees.Should().HaveCount(4);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE")!.Amount.Should().Be(50.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE")!.Amount.Should().Be(22.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE")!.Amount.Should().Be(15.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE")!.Amount.Should().Be(100.00m);
        result.TotalCost.Should().Be(1287.00m);
    }

    [Fact]
    public void CalculateTotalCost_TestCase6_Luxury1000000_ReturnsCorrectValues()
    {
        // Arrange
        var price = 1000000.00m;
        var vehicleType = VehicleType.Luxury;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(1000000.00m);
        result.AppliedFees.Should().HaveCount(4);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE")!.Amount.Should().Be(200.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE")!.Amount.Should().Be(40000.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE")!.Amount.Should().Be(20.00m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE")!.Amount.Should().Be(100.00m);
        result.TotalCost.Should().Be(1040320.00m);
    }

    [Fact]
    public void CalculateTotalCost_ExampleFromDocument_Common1000_ReturnsCorrectValues()
    {
        // Arrange
        var price = 1000m;
        var vehicleType = VehicleType.Common;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.VehicleBasePrice.Should().Be(1000m);
        result.AppliedFees.Should().HaveCount(4);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE")!.Amount.Should().Be(50m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE")!.Amount.Should().Be(20m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE")!.Amount.Should().Be(10m);
        result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE")!.Amount.Should().Be(100m);
        result.TotalCost.Should().Be(1180m);
    }

    [Fact]
    public void CalculateTotalCost_ZeroPrice_ThrowsArgumentException()
    {
        // Arrange
        var price = 0m;
        var vehicleType = VehicleType.Common;

        // Act
        var act = () => _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Vehicle price must be greater than zero*");
    }

    [Fact]
    public void CalculateTotalCost_NegativePrice_ThrowsArgumentException()
    {
        // Arrange
        var price = -100m;
        var vehicleType = VehicleType.Common;

        // Act
        var act = () => _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Vehicle price must be greater than zero*");
    }

    [Theory]
    [InlineData(VehicleType.Common)]
    [InlineData(VehicleType.Luxury)]
    public void CalculateTotalCost_ValidInput_ReturnsNonNullResult(VehicleType vehicleType)
    {
        // Arrange
        var price = 1000m;

        // Act
        var result = _service.CalculateTotalCost(price, vehicleType, GetFilteredConfig(vehicleType));

        // Assert
        result.Should().NotBeNull();
        result.TotalCost.Should().BeGreaterThan(price, "el total siempre debe incluir tarifas adicionales");
    }
}

