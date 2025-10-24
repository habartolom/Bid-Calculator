using FluentAssertions;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Domain.Services;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Domain.Services;

public class AssociationFeeCalculatorTests
{
    private readonly AssociationFeeCalculator _calculator = new();

    [Theory]
    [InlineData(398.00, 5.00)]      // range $1-$500
    [InlineData(501.00, 10.00)]     // range $501-$1000
    [InlineData(57.00, 5.00)]       // range $1-$500
    [InlineData(1800.00, 15.00)]    // range $1001-$3000
    [InlineData(1100.00, 15.00)]    // range $1001-$3000
    [InlineData(1000000.00, 20.00)] // range >$3000
    public void Calculate_PriceRanges_ReturnsCorrectFee(decimal price, decimal expectedFee)
    {
        // Arrange
        var configurations = CreateConfigurations();

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(expectedFee);
    }

    [Fact]
    public void Calculate_ThrowsException_WhenPriceIsZeroOrNegative()
    {
        // Arrange
        var configurations = CreateConfigurations();

        // Act & Assert
        var act = () => _calculator.Calculate(-100m, configurations);
        act.Should().Throw<ArgumentException>().WithMessage("*Vehicle price must be positive*");
    }

    private static IReadOnlyList<FeeConfigurationDto> CreateConfigurations()
    {
        return new List<FeeConfigurationDto>
        {
            new ()
            {
                FeeCode = "ASSOCIATION_FEE",
                VehicleTypeCode = null,
                MinVehicleValue = 0m,
                MaxVehicleValue = 500m,
                FixedAmount = 5m
            },
            new ()
            {
                FeeCode = "ASSOCIATION_FEE",
                VehicleTypeCode = null,
                MinVehicleValue = 500m,
                MaxVehicleValue = 1000m,
                FixedAmount = 10m
            },
            new ()
            {
                FeeCode = "ASSOCIATION_FEE",
                VehicleTypeCode = null,
                MinVehicleValue = 1000m,
                MaxVehicleValue = 3000m,
                FixedAmount = 15m
            },
            new ()
            {
                FeeCode = "ASSOCIATION_FEE",
                VehicleTypeCode = null,
                MinVehicleValue = 3000m,
                MaxVehicleValue = null,
                FixedAmount = 20m
            }
        };
    }
}
