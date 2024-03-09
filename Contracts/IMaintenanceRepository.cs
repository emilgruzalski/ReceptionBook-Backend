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
        IEnumerable<Maintenance> GetMaintenances(Guid roomId, bool trackChanges);
        Maintenance GetMaintenance(Guid roomId, Guid Id, bool trackChanges);
        void CreateMaintenanceForRoom(Guid roomId, Maintenance maintenance);
        void DeleteMaintenance(Maintenance maintenance);
    }
}
