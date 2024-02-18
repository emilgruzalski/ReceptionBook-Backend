using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
    }
}
