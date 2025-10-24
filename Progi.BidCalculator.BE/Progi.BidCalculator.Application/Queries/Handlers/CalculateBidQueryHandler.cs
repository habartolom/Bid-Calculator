using AutoMapper;
using MediatR;
using Progi.BidCalculator.Application.DTOs;
using Progi.BidCalculator.Domain.Interfaces;

namespace Progi.BidCalculator.Application.Queries.Handlers;

public class CalculateBidQueryHandler(
    IBidCalculatorService bidCalculatorService,
    IFeesConfigRepository feesConfigRepository,
    IMapper mapper
) : IRequestHandler<CalculateBidQuery, BidCalculationResponse>
{
    private readonly IBidCalculatorService _bidCalculatorService = bidCalculatorService ?? throw new ArgumentNullException(nameof(bidCalculatorService));
    private readonly IFeesConfigRepository _feesConfigRepository = feesConfigRepository ?? throw new ArgumentNullException(nameof(feesConfigRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<BidCalculationResponse> Handle(CalculateBidQuery request, CancellationToken cancellationToken)
    {
        var feeConfigurations = await _feesConfigRepository.GetConfigurationsByVehicleTypeAsync(request.VehicleType, cancellationToken);

        var result = _bidCalculatorService.CalculateTotalCost(
            VehiclePrice: request.VehicleBasePrice,
            VehicleType: request.VehicleType,
            FeeConfigurations: feeConfigurations
        );

        var response = _mapper.Map<BidCalculationResponse>(result);

        return response;
    }
}

