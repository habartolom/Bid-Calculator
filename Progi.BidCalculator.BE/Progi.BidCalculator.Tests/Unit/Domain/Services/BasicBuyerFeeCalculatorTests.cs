using FluentAssertions;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Domain.Services;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Domain.Services;

public class BasicBuyerFeeCalculatorTests
{
    private readonly BasicBuyerFeeCalculator _calculator = new();

    [Theory]
    [InlineData(398.00, 0.10, 10.00, 50.00, 39.80)]       // Test case 1 Common
    [InlineData(501.00, 0.10, 10.00, 50.00, 50.00)]       // Test case 2 - alcanza el máximo
    [InlineData(57.00, 0.10, 10.00, 50.00, 10.00)]        // Test case 3 - alcanza el mínimo
    [InlineData(1100.00, 0.10, 10.00, 50.00, 50.00)]      // Test case 5 - alcanza el máximo
    [InlineData(1800.00, 0.10, 25.00, 200.00, 180.00)]    // Test case 4 Luxury
    [InlineData(1000000.00, 0.10, 25.00, 200.00, 200.00)] // Test case 6 - alcanza el máximo
    public void Calculate_WithConfigurations_ReturnsCorrectFee(decimal price, decimal percentage, decimal min, decimal max, decimal expectedFee)
    {
        // Arrange
        var configurations = CreateConfigurations(percentage, min, max);

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(expectedFee);
    }

    [Fact]
    public void Calculate_CommonVehicle_AppliesMinimumFee()
    {
        // Arrange
        var price = 50m;
        var configurations = CreateConfigurations(0.10m, 10m, 50m);

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(10m, "Minimum fee should be applied");
    }

    [Fact]
    public void Calculate_CommonVehicle_AppliesMaximumFee()
    {
        // Arrange
        var price = 1000m;
        var configurations = CreateConfigurations(0.10m, 10m, 50m);

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(50m, "Maximum fee should be applied");
    }

    [Fact]
    public void Calculate_LuxuryVehicle_AppliesMinimumFee()
    {
        // Arrange
        var price = 100m;
        var configurations = CreateConfigurations(0.10m, 25m, 200m);

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(25m, "Minimum fee should be applied");
    }

    [Fact]
    public void Calculate_LuxuryVehicle_AppliesMaximumFee()
    {
        // Arrange
        var price = 100000m;
        var configurations = CreateConfigurations(0.10m, 25m, 200m);

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(200m, "Maximum fee should be applied");
    }

    [Fact]
    public void Calculate_ThrowsException_WhenPriceIsZeroOrNegative()
    {
        // Arrange
        var configurations = CreateConfigurations(0.10m, 10m, 50m);

        // Act & Assert
        var act = () => _calculator.Calculate(-100m, configurations);
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Vehicle price must be positive*");
    }

    private static IReadOnlyList<FeeConfigurationDto> CreateConfigurations(decimal percentage, decimal min, decimal max)
    {
        return new List<FeeConfigurationDto>
        {
            new ()
            {
                FeeCode = "BUYER_FEE",
                VehicleTypeCode = "COMMON",
                Percentage = percentage,
                MinAmountToApply = min,
                MaxAmountToApply = max
            }
        };
    }
}

