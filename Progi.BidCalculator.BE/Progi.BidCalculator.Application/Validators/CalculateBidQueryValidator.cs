using FluentValidation;
using Progi.BidCalculator.Application.Queries;
using Progi.BidCalculator.Domain.Enums;

namespace Progi.BidCalculator.Application.Validators;

public class CalculateBidQueryValidator : AbstractValidator<CalculateBidQuery>
{
    public CalculateBidQueryValidator()
    {
        RuleFor(x => x.VehicleBasePrice)
            .GreaterThan(0)
            .WithMessage("Vehicle price must be greater than zero")
            .LessThanOrEqualTo(decimal.MaxValue)
            .WithMessage("Vehicle price exceeds the maximum allowed value");

        RuleFor(x => x.VehicleType)
            .IsInEnum()
            .WithMessage("Vehicle type is not valid. Must be Common (1) or Luxury (2)");
    }
}

