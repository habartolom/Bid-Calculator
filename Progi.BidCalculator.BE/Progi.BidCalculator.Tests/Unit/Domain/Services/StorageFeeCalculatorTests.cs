using FluentAssertions;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Domain.Services;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Domain.Services;

public class StorageFeeCalculatorTests
{
    private readonly StorageFeeCalculator _calculator = new();

    [Theory]
    [InlineData(398.00)]
    [InlineData(501.00)]
    [InlineData(57.00)]
    [InlineData(1800.00)]
    [InlineData(1100.00)]
    [InlineData(1000000.00)]
    public void Calculate_AnyPrice_ReturnsFixed100(decimal price)
    {
        // Arrange
        var configurations = CreateConfigurations();

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(100m, "Storage fee should be fixed at $100");
    }

    private static IReadOnlyList<FeeConfigurationDto> CreateConfigurations()
    {
        return new List<FeeConfigurationDto>
        {
            new ()
            {
                FeeCode = "STORAGE_FEE",
                VehicleTypeCode = null,
                FixedAmount = 100m
            }
        };
    }
}
