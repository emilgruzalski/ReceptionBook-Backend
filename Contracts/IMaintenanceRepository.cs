using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IMaintenanceRepository
    {
        Task<PagedList<Maintenance>> GetMaintenancesAsync(Guid roomId, MaintenanceParameters maintenanceParameters, bool trackChanges);
        Task<Maintenance> GetMaintenanceAsync(Guid roomId, Guid Id, bool trackChanges);
        void CreateMaintenanceForRoom(Guid roomId, Maintenance maintenance);
        void DeleteMaintenance(Maintenance maintenance);
    }
}
