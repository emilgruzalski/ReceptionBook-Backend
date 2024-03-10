using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class MaintenanceRepository : RepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
        public async Task<IEnumerable<Maintenance>> GetMaintenancesAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(m => m.RoomId.Equals(roomId), trackChanges)
                .OrderBy(m => m.StartDate)
                .ToListAsync();
        
        public async Task<Maintenance> GetMaintenanceAsync(Guid roomId, Guid Id, bool trackChanges) =>
            await FindByCondition(m => m.RoomId.Equals(roomId) && m.Id.Equals(Id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateMaintenanceForRoom(Guid roomId, Maintenance maintenance)
        {
            maintenance.RoomId = roomId;
            Create(maintenance);
        }
        
        public void DeleteMaintenance(Maintenance maintenance) => Delete(maintenance);
    }
}
