using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync(bool trackChanges);
        Task<RoomDto> GetRoomAsync(Guid roomId, bool trackChanges);
        Task<RoomDto> CreateRoomAsync(RoomForCreationDto room);
        Task<IEnumerable<RoomDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(AvailableRoomsDto room, bool trackChanges);
        Task<(IEnumerable<RoomDto> rooms, string ids)> CreateRoomCollectionAsync(IEnumerable<RoomForCreationDto> roomCollection);
        Task DeleteRoomAsync(Guid roomId, bool trackChanges);
        Task UpdateRoomAsync(Guid roomId, RoomForUpdateDto room, bool trackChanges);
    }
}
