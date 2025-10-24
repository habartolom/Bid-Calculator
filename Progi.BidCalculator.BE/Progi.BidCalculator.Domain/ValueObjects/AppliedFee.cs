namespace Progi.BidCalculator.Domain.ValueObjects;

public sealed record AppliedFee(
    string FeeCode,
    string FeeName,
    decimal Amount,
    int DisplayOrder
);

