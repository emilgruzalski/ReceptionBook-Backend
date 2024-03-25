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
    internal sealed class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Room>> GetAllRoomsAsync(bool trackChanges, RoomParameters roomParameters)
        {
            var rooms = await FindAll(trackChanges)
                .FilterByRoomType(roomParameters.Type)
                .Search(roomParameters.SearchTerm)
                .OrderBy(r => r.Number)
                .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                .Take(roomParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).FilterByRoomType(roomParameters.Type).Search(roomParameters.SearchTerm).CountAsync();
            
            return new PagedList<Room>(rooms, count, roomParameters.PageNumber, roomParameters.PageSize);
        }

        public async Task<Room> GetRoomAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
                .SingleOrDefaultAsync();
        
        public void CreateRoom(Room room) => Create(room);
        
        public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task<PagedList<Room>> GetAvailableRoomsAsync(AvailableRoomParameters roomParameters, bool trackChanges)
        {
            var rooms = await FindByCondition(r => !r.Reservations.Any(res => res.StartDate < roomParameters.EndDate && 
                    res.EndDate > roomParameters.StartDate && 
                    res.Status != "Cancelled") && 
                    !r.Maintenances.Any(m => m.StartDate < roomParameters.EndDate && 
                    m.EndDate > roomParameters.StartDate), trackChanges)
                    .FilterByRoomType(roomParameters.Type)
                    .Search(roomParameters.SearchTerm)
                    .OrderBy(r => r.Number)
                    .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                    .Take(roomParameters.PageSize)
                    .ToListAsync();

            var count = await FindByCondition(r => !r.Reservations.Any(res => res.StartDate < roomParameters.EndDate &&
                    res.EndDate > roomParameters.StartDate &&
                    res.Status != "Cancelled") &&
                    !r.Maintenances.Any(m => m.StartDate < roomParameters.EndDate &&
                    m.EndDate > roomParameters.StartDate), trackChanges)
                    .FilterByRoomType(roomParameters.Type)
                    .Search(roomParameters.SearchTerm)
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
