using AutoMapper;
using FluentAssertions;
using Moq;
using Progi.BidCalculator.Application.Queries;
using Progi.BidCalculator.Application.Queries.Handlers;
using Progi.BidCalculator.Application.Mappings;
using Progi.BidCalculator.Domain.Enums;
using Progi.BidCalculator.Domain.Extensions;
using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Domain.ValueObjects;
using Progi.BidCalculator.Tests.Helpers;
using Xunit;

namespace Progi.BidCalculator.Tests.Unit.Application.Handlers;

public class CalculateBidQueryHandlerTests
{
    private readonly Mock<IBidCalculatorService> _mockBidCalculatorService;
    private readonly Mock<IFeesConfigRepository> _mockFeesRepo;
    private readonly IMapper _mapper;
    private readonly CalculateBidQueryHandler _handler;

    public CalculateBidQueryHandlerTests()
    {
        _mockBidCalculatorService = new Mock<IBidCalculatorService>();
        _mockFeesRepo = new Mock<IFeesConfigRepository>();
        _mockFeesRepo.Setup(r => r.GetConfigurationsByVehicleTypeAsync(It.IsAny<VehicleType>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((VehicleType vehicleType, CancellationToken _) => GetFilteredConfig(vehicleType));

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<ApplicationMappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _handler = new CalculateBidQueryHandler(_mockBidCalculatorService.Object, _mockFeesRepo.Object, _mapper);
    }

    private static IReadOnlyList<FeeConfigurationDto> GetFilteredConfig(VehicleType vehicleType)
    {
        var vehicleTypeCode = vehicleType.ToCode();
        return TestDataHelper.GetAllFeeConfigurations()
            .Where(c => c.VehicleTypeCode == null || c.VehicleTypeCode == vehicleTypeCode)
            .ToList();
    }

    [Fact]
    public async Task Handle_ValidQuery_CallsServiceAndReturnsResponse()
    {
        // Arrange
        var query = new CalculateBidQuery(1000m, VehicleType.Common);
        var appliedFees = new List<AppliedFee>
        {
            new (FeeCode: "BUYER_FEE", FeeName: "Basic Buyer Fee", Amount: 50m, DisplayOrder: 1),
            new (FeeCode: "SPECIAL_FEE", FeeName: "Special Fee", Amount :20m, DisplayOrder: 2),
            new (FeeCode: "ASSOCIATION_FEE", FeeName: "Association Fee", Amount :10m, DisplayOrder: 3),
            new (FeeCode: "STORAGE_FEE", FeeName: "Storage Fee", Amount :100m, DisplayOrder: 4)
        };
        var expectedResult = new BidCalculationResult(
            VehicleBasePrice: 1000m,
            AppliedFees: appliedFees
        );

        _mockBidCalculatorService
            .Setup(s => s.CalculateTotalCost(query.VehicleBasePrice, query.VehicleType, It.IsAny<IReadOnlyList<FeeConfigurationDto>>()))
            .Returns(expectedResult);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.VehicleBasePrice.Should().Be(1000m);
        response.AppliedFees.Should().HaveCount(4);
        
        var buyerFee = response.AppliedFees.FirstOrDefault(f => f.FeeCode == "BUYER_FEE");
        buyerFee.Should().NotBeNull();
        buyerFee!.FeeName.Should().Be("Basic Buyer Fee");
        buyerFee.Amount.Should().Be(50m);
        buyerFee.DisplayOrder.Should().Be(1);
        
        var specialFee = response.AppliedFees.FirstOrDefault(f => f.FeeCode == "SPECIAL_FEE");
        specialFee.Should().NotBeNull();
        specialFee!.Amount.Should().Be(20m);
        
        var associationFee = response.AppliedFees.FirstOrDefault(f => f.FeeCode == "ASSOCIATION_FEE");
        associationFee.Should().NotBeNull();
        associationFee!.Amount.Should().Be(10m);
        
        var storageFee = response.AppliedFees.FirstOrDefault(f => f.FeeCode == "STORAGE_FEE");
        storageFee.Should().NotBeNull();
        storageFee!.Amount.Should().Be(100m);
        
        response.TotalCost.Should().Be(1180m);

        _mockBidCalculatorService.Verify(
            s => s.CalculateTotalCost(query.VehicleBasePrice, query.VehicleType, It.IsAny<IReadOnlyList<FeeConfigurationDto>>()),
            Times.Once
        );
        _mockFeesRepo.Verify(r => r.GetConfigurationsByVehicleTypeAsync(query.VehicleType, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ServiceThrowsException_PropagatesException()
    {
        // Arrange
        var query = new CalculateBidQuery(0m, VehicleType.Common);
        var expectedException = new ArgumentException("Invalid price");

        _mockBidCalculatorService
            .Setup(s => s.CalculateTotalCost(It.IsAny<decimal>(), It.IsAny<VehicleType>(), It.IsAny<IReadOnlyList<FeeConfigurationDto>>()))
            .Throws(expectedException);

        // Act
        var act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid price");
    }

    [Theory]
    [InlineData(VehicleType.Common)]
    [InlineData(VehicleType.Luxury)]
    public async Task Handle_DifferentVehicleTypes_CallsServiceWithCorrectType(VehicleType vehicleType)
    {
        // Arrange
        var query = new CalculateBidQuery(1000m, vehicleType);
        var appliedFees = new List<AppliedFee>
        {
            new (FeeCode: "BUYER_FEE", FeeName: "Basic Buyer Fee", Amount: 50m, DisplayOrder: 1 ),
            new (FeeCode: "SPECIAL_FEE", FeeName: "Special Fee", Amount: 20m, DisplayOrder: 2 ),
            new (FeeCode: "ASSOCIATION_FEE", FeeName: "Association Fee", Amount: 10m, DisplayOrder: 3 ),
            new (FeeCode: "STORAGE_FEE", FeeName: "Storage Fee", Amount: 100m, DisplayOrder: 4 ),
        };
        var result = new BidCalculationResult(1000m, appliedFees);

        _mockBidCalculatorService
            .Setup(s => s.CalculateTotalCost(It.IsAny<decimal>(), It.IsAny<VehicleType>(), It.IsAny<IReadOnlyList<FeeConfigurationDto>>()))
            .Returns(result);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockBidCalculatorService.Verify(
            s => s.CalculateTotalCost(1000m, vehicleType, It.IsAny<IReadOnlyList<FeeConfigurationDto>>()),
            Times.Once
        );
    }

    [Fact]
    public void Constructor_NullService_ThrowsArgumentNullException()
    {
        // Act
        var act = () => new CalculateBidQueryHandler(null!, _mockFeesRepo.Object, _mapper);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_NullRepository_ThrowsArgumentNullException()
    {
        // Act
        var act = () => new CalculateBidQueryHandler(_mockBidCalculatorService.Object, null!, _mapper);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_NullMapper_ThrowsArgumentNullException()
    {
        // Act
        var act = () => new CalculateBidQueryHandler(_mockBidCalculatorService.Object, _mockFeesRepo.Object, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}

