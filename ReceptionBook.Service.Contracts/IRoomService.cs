using ReceptionBook.Shared.DataTransferObjects;

namespace ReceptionBook.Service.Contracts
{
    public interface IRoomService
    {
        IEnumerable<RoomDto> GetAllRooms(bool trackChanges);
    }
}
