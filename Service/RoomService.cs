using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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

        public IEnumerable<RoomDto> GetAllRooms(bool trackChanges)
        {
            var rooms = _repository.Room.GetAllRooms(trackChanges);

            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            return roomsDto;
        }
        
        public RoomDto GetRoom(Guid id, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(id, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(id);
            
            var roomDto = _mapper.Map<RoomDto>(room);
            return roomDto;
        }
        
        public RoomDto CreateRoom(RoomForCreationDto room)
        {
            var roomEntity = _mapper.Map<Room>(room);
            
            _repository.Room.CreateRoom(roomEntity);
            _repository.Save();
            
            var roomToReturn = _mapper.Map<RoomDto>(roomEntity);
            
            return roomToReturn;
        }
        
        public IEnumerable<RoomDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            
            var roomsEntities = _repository.Room.GetByIds(ids, trackChanges);
            if (ids.Count() != roomsEntities.Count())
                throw new CollectionByIdsBadRequestException();
            
            var roomsToReturn = _mapper.Map<IEnumerable<RoomDto>>(roomsEntities);
            
            return roomsToReturn;
        }
        
        public IEnumerable<RoomDto> GetAvailableRooms(AvailableRoomsDto room, bool trackChanges)
        {
            var startDate = room.StartDate;
            var endDate = room.EndDate;
            
            var roomsEntities = _repository.Room.GetAvailableRooms(startDate, endDate, trackChanges);
            var roomsToReturn = _mapper.Map<IEnumerable<RoomDto>>(roomsEntities);
            
            return roomsToReturn;
        }

        public (IEnumerable<RoomDto> rooms, string ids) CreateRoomCollection(IEnumerable<RoomForCreationDto> roomCollection)
        {
            if (roomCollection is null)
                throw new RoomCollectionBadRequest();

            var roomEntities = _mapper.Map<IEnumerable<Room>>(roomCollection);
            foreach (var room in roomEntities)
            {
                _repository.Room.CreateRoom(room);
            }

            _repository.Save();

            var roomCollectionToReturn = _mapper.Map<IEnumerable<RoomDto>>(roomEntities);
            var ids = string.Join(",", roomCollectionToReturn.Select(r => r.Id));

            return (rooms: roomCollectionToReturn, ids: ids);
        }
        
        public void DeleteRoom(Guid id, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(id, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(id);
            
            _repository.Room.DeleteRoom(room);
            _repository.Save();
        }
    }
}
