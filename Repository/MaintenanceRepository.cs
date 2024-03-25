using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class MaintenanceRepository : RepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Maintenance>> GetMaintenancesAsync(Guid roomId,
            MaintenanceParameters maintenanceParameters, bool trackChanges)
        {
            var maintenances = await FindByCondition(m => m.RoomId.Equals(roomId), trackChanges)
                .Search(maintenanceParameters.SearchTerm)
                .OrderBy(m => m.StartDate)
                .Skip((maintenanceParameters.PageNumber - 1) * maintenanceParameters.PageSize)
                .Take(maintenanceParameters.PageSize)
                .ToListAsync();
            
            var count = await FindByCondition(m => m.RoomId.Equals(roomId), trackChanges).CountAsync();
            
            return new PagedList<Maintenance>(maintenances, count, maintenanceParameters.PageNumber, maintenanceParameters.PageSize);
        }

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
