using ReceptionBook.Contracts;
using ReceptionBook.Service.Contracts;

namespace ReceptionBook.Service
{
    internal sealed class RoomService : IRoomService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public RoomService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
