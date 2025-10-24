using FluentAssertions;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Domain.Services;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Domain.Services;

public class SpecialFeeCalculatorTests
{
    private readonly SpecialFeeCalculator _calculator = new();

    [Theory]
    [InlineData(398.00, 0.02, 7.96)]            // 2% of 398
    [InlineData(501.00, 0.02, 10.02)]           // 2% of 501
    [InlineData(1100.00, 0.02, 22.00)]          // 2% of 1100
    [InlineData(1800.00, 0.04, 72.00)]          // 4% of 1800
    [InlineData(1000000.00, 0.04, 40000.00)]    // 4% of 1000000
    public void Calculate_WithConfigurations_ReturnsCorrectFee(decimal price, decimal percentage, decimal expectedFee)
    {
        // Arrange
        var configurations = CreateConfigurations(percentage);

        // Act
        var result = _calculator.Calculate(price, configurations);

        // Assert
        result.Should().Be(expectedFee);
    }

    [Fact]
    public void Calculate_ThrowsException_WhenPriceIsZeroOrNegative()
    {
        // Arrange
        var configurations = CreateConfigurations(0.02m);

        // Act & Assert
        var act = () => _calculator.Calculate(-100m, configurations);
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Vehicle price must be positive*");
    }

    private static IReadOnlyList<FeeConfigurationDto> CreateConfigurations(decimal percentage)
    {
        return new List<FeeConfigurationDto>
        {
            new()
            {
                FeeCode = "SPECIAL_FEE",
                VehicleTypeCode = "COMMON",
                Percentage = percentage
            }
        };
    }
}
