using Microsoft.EntityFrameworkCore;
using Progi.BidCalculator.Domain.Enums;
using Progi.BidCalculator.Domain.Extensions;
using Progi.BidCalculator.Domain.Interfaces;
using Progi.BidCalculator.Domain.Models;
using Progi.BidCalculator.Infrastructure.Persistence;

namespace Progi.BidCalculator.Infrastructure.Repositories;

public class FeesConfigRepository(BidCalculatorDbContext dbContext) : IFeesConfigRepository
{
    public async Task<IReadOnlyList<FeeConfigurationDto>> GetConfigurationsByVehicleTypeAsync(VehicleType vehicleType, CancellationToken cancellationToken)
    {
        var vehicleTypeCode = vehicleType.ToCode();

        var feeConfigurations = await dbContext.FeeConfigurations
            .AsNoTracking()
            .Include(fc => fc.FeeType)
            .Include(fc => fc.VehicleType)
            .Where(fc => 
                fc.FeeType.IsActive &&
                (fc.VehicleType == null || fc.VehicleType.Code == vehicleTypeCode))
            .OrderBy(fc => fc.FeeType.DisplayOrder)
            .ToListAsync(cancellationToken);

        if (feeConfigurations.Count == 0)
            throw new InvalidOperationException($"No fee configurations found for vehicle type '{vehicleTypeCode}'");

        var configurations = feeConfigurations
            .Select(fc => new FeeConfigurationDto
            {
                FeeCode = fc.FeeType.Code,
                FeeName = fc.FeeType.Name,
                DisplayOrder = fc.FeeType.DisplayOrder,
                VehicleTypeCode = fc.VehicleType?.Code,
                Percentage = fc.Percentage,
                MinAmountToApply = fc.MinAmountToApply,
                MaxAmountToApply = fc.MaxAmountToApply,
                FixedAmount = fc.FixedAmount,
                MinVehicleValue = fc.MinVehicleValue,
                MaxVehicleValue = fc.MaxVehicleValue
            })
            .ToList();

        return configurations;
    }
}
