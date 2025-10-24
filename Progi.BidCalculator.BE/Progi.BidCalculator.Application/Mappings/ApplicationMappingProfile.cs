using AutoMapper;
using Progi.BidCalculator.Application.DTOs;
using Progi.BidCalculator.Domain.ValueObjects;

namespace Progi.BidCalculator.Application.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<AppliedFee, AppliedFeeDto>();
        CreateMap<BidCalculationResult, BidCalculationResponse>()
            .ForMember(dest => dest.AppliedFees, opt => opt.MapFrom(src => src.AppliedFees));
    }
}

