using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Room>> GetAllRoomsAsync(bool trackChanges, RoomParameters roomParameters)
        {
            var rooms = await FindAll(trackChanges)
                .OrderBy(r => r.Number)
                .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                .Take(roomParameters.PageSize)
                .ToListAsync();
            
            var count = await FindAll(trackChanges).CountAsync();
            
            return new PagedList<Room>(rooms, count, roomParameters.PageNumber, roomParameters.PageSize);
        }

        public async Task<Room> GetRoomAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
            .SingleOrDefaultAsync();
        
        public void CreateRoom(Room room) => Create(room);
        
        public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task<PagedList<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate,
            RoomParameters roomParameters, bool trackChanges)
        {
            var rooms = await FindByCondition(r => !r.Reservations.Any(res => res.StartDate < endDate &&
                                                                  res.EndDate > startDate &&
                                                                  res.Status != "Cancelled") &&
                                       !r.Maintenances.Any(m => m.StartDate < endDate &&
                                                                m.EndDate > startDate), trackChanges)
                .OrderBy(r => r.Number)
                .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                .Take(roomParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(r => !r.Reservations.Any(res => res.StartDate < endDate &&
                                                                              res.EndDate > startDate &&
                                                                              res.Status != "Cancelled") &&
                                                   !r.Maintenances.Any(m => m.StartDate < endDate &&
                                                                            m.EndDate > startDate), trackChanges)
                .CountAsync();
            
            return new PagedList<Room>(rooms, count, roomParameters.PageNumber, roomParameters.PageSize);
        }

        public async Task<Room> GetRoomWithDetailsAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
                .Include(r => r.Reservations)
                .Include(r => r.Maintenances)
                .SingleOrDefaultAsync();
        
        public void DeleteRoom(Room room) => Delete(room);
    }
}
