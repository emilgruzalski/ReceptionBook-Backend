using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IRoomService
    {
        Task<(IEnumerable<RoomDto> rooms, MetaData metaData)> GetAllRoomsAsync(bool trackChanges, RoomParameters roomParameters);
        Task<RoomDto> GetRoomAsync(Guid roomId, bool trackChanges);
        Task<RoomDto> CreateRoomAsync(RoomForCreationDto room);
        Task<IEnumerable<RoomDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<RoomDto> rooms, MetaData metaData)> GetAvailableRoomsAsync(AvailableRoomParameters roomParameters, bool trackChanges);
        Task<(IEnumerable<RoomDto> rooms, string ids)> CreateRoomCollectionAsync(IEnumerable<RoomForCreationDto> roomCollection);
        Task DeleteRoomAsync(Guid roomId, bool trackChanges);
        Task UpdateRoomAsync(Guid roomId, RoomForUpdateDto room, bool trackChanges);
    }
}
