using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges);
        Task<Room> GetRoomAsync(Guid roomId, bool trackChanges);
        void CreateRoom(Room room);
        Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, bool trackChanges);
        Task<Room> GetRoomWithDetailsAsync(Guid roomId, bool trackChanges);
        void DeleteRoom(Room room);
    }
}
