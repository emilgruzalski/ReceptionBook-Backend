using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class RoomService : IRoomService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RoomService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<RoomDto> rooms, MetaData metaData)> GetAllRoomsAsync(bool trackChanges, RoomParameters roomParameters)
        {
            var roomsWithMetaData = await _repository.Room.GetAllRoomsAsync(trackChanges, roomParameters);
            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(roomsWithMetaData);

            return (rooms: roomsDto, metaData: roomsWithMetaData.MetaData);
        }
        
        public async Task<RoomDto> GetRoomAsync(Guid id, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(id, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(id);
            
            var roomDto = _mapper.Map<RoomDto>(room);
            return roomDto;
        }
        
        public async Task<RoomDto> CreateRoomAsync(RoomForCreationDto room)
        {
            var roomEntity = _mapper.Map<Room>(room);
            
            _repository.Room.CreateRoom(roomEntity);
            await _repository.SaveAsync();
            
            var roomToReturn = _mapper.Map<RoomDto>(roomEntity);
            
            return roomToReturn;
        }
        
        public async Task<IEnumerable<RoomDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            
            var roomsEntities = await _repository.Room.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != roomsEntities.Count())
                throw new CollectionByIdsBadRequestException();
            
            var roomsToReturn = _mapper.Map<IEnumerable<RoomDto>>(roomsEntities);
            
            return roomsToReturn;
        }
        
        public async Task<(IEnumerable<RoomDto> rooms, MetaData metaData)> GetAvailableRoomsAsync(AvailableRoomParameters roomParameters, bool trackChanges)
        {
            if (!roomParameters.ValidDateRange)
                throw new EndDateRangeBadRequestException();

            var roomsWithMetaData = await _repository.Room.GetAvailableRoomsAsync(roomParameters, trackChanges);
            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(roomsWithMetaData);
            
            return (rooms: roomsDto, metaData: roomsWithMetaData.MetaData);
        }

        public async Task<(IEnumerable<RoomDto> rooms, string ids)> CreateRoomCollectionAsync(IEnumerable<RoomForCreationDto> roomCollection)
        {
            if (roomCollection is null)
                throw new RoomCollectionBadRequest();

            var roomEntities = _mapper.Map<IEnumerable<Room>>(roomCollection);
            foreach (var room in roomEntities)
            {
                _repository.Room.CreateRoom(room);
            }

            await _repository.SaveAsync();

            var roomCollectionToReturn = _mapper.Map<IEnumerable<RoomDto>>(roomEntities);
            var ids = string.Join(",", roomCollectionToReturn.Select(r => r.Id));

            return (rooms: roomCollectionToReturn, ids: ids);
        }
        
        public async Task DeleteRoomAsync(Guid id, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(id, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(id);
            
            _repository.Room.DeleteRoom(room);
            await _repository.SaveAsync();
        }
        
        public async Task UpdateRoomAsync(Guid id, RoomForUpdateDto room, bool trackChanges)
        {
            var roomEntity = await _repository.Room.GetRoomAsync(id, trackChanges);
            if (roomEntity is null)
                throw new RoomNotFoundException(id);
            
            _mapper.Map(room, roomEntity);
            await _repository.SaveAsync();
        }
    }
}
