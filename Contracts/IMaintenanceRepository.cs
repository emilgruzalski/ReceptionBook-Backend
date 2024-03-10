using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IMaintenanceRepository
    {
        Task<IEnumerable<Maintenance>> GetMaintenancesAsync(Guid roomId, bool trackChanges);
        Task<Maintenance> GetMaintenanceAsync(Guid roomId, Guid Id, bool trackChanges);
        void CreateMaintenanceForRoom(Guid roomId, Maintenance maintenance);
        void DeleteMaintenance(Maintenance maintenance);
    }
}
