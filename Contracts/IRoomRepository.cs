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
        IEnumerable<Room> GetAllRooms(bool trackChanges);
        Room GetRoom(Guid roomId, bool trackChanges);
        void CreateRoom(Room room);
        IEnumerable<Room> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        IEnumerable<Room> GetAvailableRooms(DateTime startDate, DateTime endDate, bool trackChanges);
        Room GetRoomWithDetails(Guid roomId, bool trackChanges);
    }
}
