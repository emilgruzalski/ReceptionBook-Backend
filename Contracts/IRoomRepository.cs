using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IRoomRepository
    {
        Task<PagedList<Room>> GetAllRoomsAsync(bool trackChanges, RoomParameters roomParameters);
        Task<Room> GetRoomWithDetailsAsync(Guid roomId, bool trackChanges);
        void CreateRoom(Room room);
        Task<IEnumerable<Room>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<PagedList<Room>> GetAvailableRoomsAsync(AvailableRoomParameters roomParameters, bool trackChanges);
        Task<PagedList<Room>> GetAvailableRoomsAsync(Guid reservationId, AvailableRoomParameters roomParameters, bool trackChanges);
        void DeleteRoom(Room room);
        Task<bool> RoomNumberExistsAsync(string roomNumber);
        Task<bool> RoomNumberExistsAsync(Guid id, string roomNumber);
    }
}
