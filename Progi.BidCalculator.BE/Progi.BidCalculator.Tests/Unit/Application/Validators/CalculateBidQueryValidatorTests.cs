using FluentAssertions;
using Progi.BidCalculator.Application.Queries;
using Progi.BidCalculator.Application.Validators;
using Progi.BidCalculator.Domain.Enums;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Application.Validators;

public class CalculateBidQueryValidatorTests
{
    private readonly CalculateBidQueryValidator _validator;

    public CalculateBidQueryValidatorTests()
    {
        _validator = new CalculateBidQueryValidator();
    }

    [Fact]
    public void Validate_ValidQuery_ReturnsNoErrors()
    {
        // Arrange
        var query = new CalculateBidQuery(1000m, VehicleType.Common);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_ZeroOrNegativePrice_ReturnsError(decimal price)
    {
        // Arrange
        var query = new CalculateBidQuery(price, VehicleType.Common);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(query.VehicleBasePrice));
    }

    [Fact]
    public void Validate_InvalidVehicleType_ReturnsError()
    {
        // Arrange
        var query = new CalculateBidQuery(1000m, (VehicleType)999);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(query.VehicleType));
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(100)]
    [InlineData(1000000)]
    public void Validate_PositivePrice_ReturnsNoErrors(decimal price)
    {
        // Arrange
        var query = new CalculateBidQuery(price, VehicleType.Common);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(VehicleType.Common)]
    [InlineData(VehicleType.Luxury)]
    public void Validate_ValidVehicleTypes_ReturnsNoErrors(VehicleType vehicleType)
    {
        // Arrange
        var query = new CalculateBidQuery(1000m, vehicleType);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}

