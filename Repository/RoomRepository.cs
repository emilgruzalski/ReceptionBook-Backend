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
    internal sealed class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(r => r.Number)
                .ToListAsync();
        
        public async Task<Room> GetRoomAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
            .SingleOrDefaultAsync();
        
        public void CreateRoom(Room room) => Create(room);
        
        public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        
        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, bool trackChanges) =>
            await FindByCondition(r => !r.Reservations.Any(res => res.StartDate < endDate && 
                                                            res.EndDate > startDate && 
                                                            res.Status != "Cancelled") &&
                                 !r.Maintenances.Any(m => m.StartDate < endDate && 
                                                          m.EndDate > startDate), trackChanges)
                .ToListAsync();
        
        public async Task<Room> GetRoomWithDetailsAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
                .Include(r => r.Reservations)
                .Include(r => r.Maintenances)
                .SingleOrDefaultAsync();
        
        public void DeleteRoom(Room room) => Delete(room);
    }
}
