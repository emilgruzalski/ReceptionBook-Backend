using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRoomService
    {
        IEnumerable<RoomDto> GetAllRooms(bool trackChanges);
        RoomDto GetRoom(Guid roomId, bool trackChanges);
        RoomDto CreateRoom(RoomForCreationDto room);
        IEnumerable<RoomDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        IEnumerable<RoomDto> GetAvailableRooms(AvailableRoomsDto room, bool trackChanges);
        (IEnumerable<RoomDto> rooms, string ids) CreateRoomCollection(IEnumerable<RoomForCreationDto> roomCollection);
        void DeleteRoom(Guid roomId, bool trackChanges);
    }
}
