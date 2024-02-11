using AutoMapper;
using ReceptionBook.Contracts;
using ReceptionBook.Service.Contracts;
using ReceptionBook.Shared.DataTransferObjects;

namespace ReceptionBook.Service
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
            try
            {
                var rooms = _repository.Room.GetAllRooms(trackChanges);

                var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

                return roomsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllRooms)} service method {ex}");
                throw;
            }
        }
    }
}
