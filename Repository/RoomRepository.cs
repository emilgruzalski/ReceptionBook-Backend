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
                .FilterRooms(roomParameters.Type)
                .Search(roomParameters.SearchTerm)
                .Sort(roomParameters.OrderBy)
                .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                .Take(roomParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).FilterRooms(roomParameters.Type).Search(roomParameters.SearchTerm).CountAsync();
            
            return new PagedList<Room>(rooms, count, roomParameters.PageNumber, roomParameters.PageSize);
        }
        
        public void CreateRoom(Room room) => Create(room);
        
        public async Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task<PagedList<Room>> GetAvailableRoomsAsync(AvailableRoomParameters roomParameters, bool trackChanges)
        {
            var rooms = await FindByCondition(r => !r.Reservations.Any(res => res.StartDate <= roomParameters.EndDate && 
                    res.EndDate >= roomParameters.StartDate && res.Status != "Cancelled"), trackChanges)
                    .FilterRooms(roomParameters.Type)
                    .Search(roomParameters.SearchTerm)
                    .Sort(roomParameters.OrderBy)
                    .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                    .Take(roomParameters.PageSize)
                    .ToListAsync();

            var count = await FindByCondition(r => !r.Reservations.Any(res => res.StartDate <= roomParameters.EndDate &&
                    res.EndDate >= roomParameters.StartDate), trackChanges)
                    .FilterRooms(roomParameters.Type)
                    .Search(roomParameters.SearchTerm)
                    .CountAsync();
            
            return new PagedList<Room>(rooms, count, roomParameters.PageNumber, roomParameters.PageSize);
        }

        public async Task<PagedList<Room>> GetAvailableRoomsAsync(Guid reservationId, AvailableRoomParameters roomParameters, bool trackChanges)
        {
            var rooms = await FindByCondition(r =>
                !r.Reservations.Any(res => res.StartDate < roomParameters.EndDate &&
                                           res.EndDate > roomParameters.StartDate &&
                                           res.Status != "Cancelled" &&
                                           !res.Id.Equals(reservationId )) ||
                (r.Reservations.Any(res => res.Id.Equals(reservationId))),
                trackChanges)
                .FilterRooms(roomParameters.Type)
                .Search(roomParameters.SearchTerm)
                .Sort(roomParameters.OrderBy)
                .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
                .Take(roomParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(r =>
                !r.Reservations.Any(res => res.StartDate < roomParameters.EndDate &&
                                           res.EndDate > roomParameters.StartDate &&
                                           !res.Id.Equals(reservationId)) ||
                (r.Reservations.Any(res => res.Id.Equals(reservationId)) &&
                 !r.Reservations.Any(res => !res.Id.Equals(reservationId) &&
                                           res.StartDate < roomParameters.EndDate &&
                                           res.EndDate > roomParameters.StartDate)),
                trackChanges)
                .FilterRooms(roomParameters.Type)
                .Search(roomParameters.SearchTerm)
                .CountAsync();

            return new PagedList<Room>(rooms, count, roomParameters.PageNumber, roomParameters.PageSize);
        }



        public async Task<Room> GetRoomWithDetailsAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
                .Include(r => r.Reservations)
                .SingleOrDefaultAsync();
        
        public void DeleteRoom(Room room) => Delete(room);

        public async Task<bool> RoomNumberExistsAsync(string roomNumber) =>
            await FindByCondition(r => r.Number.Equals(roomNumber), false).AnyAsync();

        public async Task<bool> RoomNumberExistsAsync(Guid id, string roomNumber) =>
            await FindByCondition(r => r.Number.Equals(roomNumber) && !r.Id.Equals(id), false).AnyAsync();
    }
}
