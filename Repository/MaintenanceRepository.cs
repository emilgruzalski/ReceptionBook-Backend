using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Repository
{
    internal sealed class MaintenanceRepository : RepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
        public IEnumerable<Maintenance> GetMaintenances(Guid roomId, bool trackChanges) =>
            FindByCondition(m => m.RoomId.Equals(roomId), trackChanges)
                .OrderBy(m => m.StartDate)
                .ToList();
        
        public Maintenance GetMaintenance(Guid roomId, Guid Id, bool trackChanges) =>
            FindByCondition(m => m.RoomId.Equals(roomId) && m.Id.Equals(Id), trackChanges)
                .SingleOrDefault();

        public void CreateMaintenanceForRoom(Guid roomId, Maintenance maintenance)
        {
            maintenance.RoomId = roomId;
            Create(maintenance);
        }
        
        public void DeleteMaintenance(Maintenance maintenance) => Delete(maintenance);
    }
}
