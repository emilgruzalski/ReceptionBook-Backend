using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IMaintenanceService
    {
        Task<(IEnumerable<MaintenanceDto> maintenances, MetaData metaData)> GetMaintenancesAsync(Guid roomId, MaintenanceParameters maintenanceParameters, bool trackChanges);
        Task<MaintenanceDto> GetMaintenanceAsync(Guid roomId, Guid Id, bool trackChanges);
        Task<MaintenanceDto> CreateMaintenanceForRoomAsync(Guid roomId, MaintenanceForCreationDto maintenance, bool trackChanges);
        Task DeleteMaintenanceForRoomAsync(Guid roomId, Guid id, bool trackChanges);
        Task UpdateMaintenanceForRoomAsync(Guid roomId, Guid id, MaintenanceForUpdateDto maintenance, bool roomTrackChanges, bool mainTrackChanges);
    }
}
