using Progi.BidCalculator.Domain.Enums;
using Progi.BidCalculator.Domain.Models;

namespace Progi.BidCalculator.Domain.Interfaces;

public interface IFeesConfigRepository
{
    Task<IReadOnlyList<FeeConfigurationDto>> GetConfigurationsByVehicleTypeAsync(VehicleType vehicleType, CancellationToken cancellationToken);
}


