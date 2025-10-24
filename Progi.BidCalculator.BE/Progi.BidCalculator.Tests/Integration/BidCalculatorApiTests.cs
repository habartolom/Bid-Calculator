using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Progi.BidCalculator.Application.DTOs;
using Progi.BidCalculator.Domain.Enums;
using Xunit;

namespace Progi.BidCalculator.Tests.Integration;

public class BidCalculatorApiTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Calculate_TestCase1_Common398_ReturnsCorrectResult()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 398.00m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        
        result.Should().NotBeNull();
        result!.VehicleBasePrice.Should().Be(398.00m);
        result.AppliedFees.Should().HaveCount(4);
        
        var buyerFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE");
        buyerFee.Should().NotBeNull();
        buyerFee!.Amount.Should().Be(39.80m);
        
        var specialFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE");
        specialFee.Should().NotBeNull();
        specialFee!.Amount.Should().Be(7.96m);
        
        var associationFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE");
        associationFee.Should().NotBeNull();
        associationFee!.Amount.Should().Be(5.00m);
        
        var storageFee = result.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE");
        storageFee.Should().NotBeNull();
        storageFee!.Amount.Should().Be(100.00m);
        
        result.TotalCost.Should().Be(550.76m);
    }

    [Fact]
    public async Task Calculate_TestCase2_Common501_ReturnsCorrectResult()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 501.00m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        
        result.Should().NotBeNull();
        result!.TotalCost.Should().Be(671.02m);
    }

    [Fact]
    public async Task Calculate_TestCase3_Common57_ReturnsCorrectResult()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 57.00m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        
        result.Should().NotBeNull();
        result!.TotalCost.Should().Be(173.14m);
    }

    [Fact]
    public async Task Calculate_TestCase4_Luxury1800_ReturnsCorrectResult()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 1800.00m,
            VehicleType = VehicleType.Luxury
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        
        result.Should().NotBeNull();
        result!.TotalCost.Should().Be(2167.00m);
    }

    [Fact]
    public async Task Calculate_TestCase5_Common1100_ReturnsCorrectResult()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 1100.00m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        
        result.Should().NotBeNull();
        result!.TotalCost.Should().Be(1287.00m);
    }

    [Fact]
    public async Task Calculate_TestCase6_Luxury1000000_ReturnsCorrectResult()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 1000000.00m,
            VehicleType = VehicleType.Luxury
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        
        result.Should().NotBeNull();
        result!.TotalCost.Should().Be(1040320.00m);
    }

    [Fact]
    public async Task Calculate_InvalidPrice_ReturnsBadRequest()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = -100m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Calculate_ZeroPrice_ReturnsBadRequest()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 0m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Health_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/BidCalculator/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Healthy");
    }

    [Fact]
    public async Task Calculate_ValidRequest_ReturnsJsonContentType()
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = 1000m,
            VehicleType = VehicleType.Common
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
    }

    [Theory]
    [InlineData(100, VehicleType.Common)]
    [InlineData(1000, VehicleType.Common)]
    [InlineData(100, VehicleType.Luxury)]
    [InlineData(1000, VehicleType.Luxury)]
    public async Task Calculate_VariousPricesAndTypes_ReturnsOk(decimal price, VehicleType vehicleType)
    {
        // Arrange
        var request = new BidCalculationRequest
        {
            VehicleBasePrice = price,
            VehicleType = vehicleType
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BidCalculator/calculate", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<BidCalculationResponse>();
        result.Should().NotBeNull();
        result!.TotalCost.Should().BeGreaterThan(price);
    }
}

