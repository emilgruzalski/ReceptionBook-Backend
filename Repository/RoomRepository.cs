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

        public IEnumerable<Room> GetAllRooms(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(r => r.Number)
                .ToList();
        
        public Room GetRoom(Guid roomId, bool trackChanges) =>
            FindByCondition(r => r.Id.Equals(roomId), trackChanges)
            .SingleOrDefault();
        
        public void CreateRoom(Room room) => Create(room);
        
        public IEnumerable<Room> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();
        
        public IEnumerable<Room> GetAvailableRooms(DateTime startDate, DateTime endDate, bool trackChanges) =>
            FindByCondition(r => !r.Reservations.Any(res => res.StartDate < endDate && 
                                                            res.EndDate > startDate && 
                                                            res.Status != "Cancelled") &&
                                 !r.Maintenances.Any(m => m.StartDate < endDate && 
                                                          m.EndDate > startDate), trackChanges)
                .ToList();
        
        public Room GetRoomWithDetails(Guid roomId, bool trackChanges) =>
            FindByCondition(r => r.Id.Equals(roomId), trackChanges)
                .Include(r => r.Reservations)
                .Include(r => r.Maintenances)
                .SingleOrDefault();
        
        public void DeleteRoom(Room room) => Delete(room);
    }
}
