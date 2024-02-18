using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ReceptionBook.Contracts;
using ReceptionBook.Entities.Exceptions;
using ReceptionBook.Service.Contracts;
using ReceptionBook.Shared.DataTransferObjects;

namespace ReceptionBook.Service
{
    internal sealed class MaintenanceService : IMaintenanceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaintenanceService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public IEnumerable<MaintenanceDto> GetMaintenances(Guid roomId, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var maintenanceFromDb = _repository.Maintenance.GetMaintenances(roomId, trackChanges);
            var maintenanceDto = _mapper.Map<IEnumerable<MaintenanceDto>>(maintenanceFromDb);
            
            return maintenanceDto;
        }

        public MaintenanceDto GetMaintenance(Guid roomId, Guid id, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);

            var maintenanceDb = _repository.Maintenance.GetMaintenance(roomId, id, trackChanges);
            if (maintenanceDb is null)
                throw new MaintenanceNotFoundException(id);

            var maintenance = _mapper.Map<MaintenanceDto>(maintenanceDb);

            return maintenance;
        }
    }
}
