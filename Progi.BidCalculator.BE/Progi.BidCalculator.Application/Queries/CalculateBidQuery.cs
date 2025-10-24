using MediatR;
using Progi.BidCalculator.Application.DTOs;
using Progi.BidCalculator.Domain.Enums;

namespace Progi.BidCalculator.Application.Queries;

public sealed record CalculateBidQuery(
    decimal VehicleBasePrice, 
    VehicleType VehicleType
) : IRequest<BidCalculationResponse>;

