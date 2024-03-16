using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
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
        
        public async Task<(IEnumerable<MaintenanceDto> maintenances, MetaData metaData)> GetMaintenancesAsync(Guid roomId, MaintenanceParameters maintenanceParameters, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var maintenanceWithMetaData = await _repository.Maintenance.GetMaintenancesAsync(roomId, maintenanceParameters, trackChanges);
            var maintenanceDto = _mapper.Map<IEnumerable<MaintenanceDto>>(maintenanceWithMetaData);
            
            return (maintenances: maintenanceDto, metaData: maintenanceWithMetaData.MetaData);
        }

        public async Task<MaintenanceDto> GetMaintenanceAsync(Guid roomId, Guid id, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);

            var maintenanceDb = await _repository.Maintenance.GetMaintenanceAsync(roomId, id, trackChanges);
            if (maintenanceDb is null)
                throw new MaintenanceNotFoundException(id);

            var maintenance = _mapper.Map<MaintenanceDto>(maintenanceDb);

            return maintenance;
        }
        
        public async Task<MaintenanceDto> CreateMaintenanceForRoomAsync(Guid roomId, MaintenanceForCreationDto maintenanceForCreation, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomWithDetailsAsync(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            if (room.Reservations.Any(r => r.StartDate <= maintenanceForCreation.EndDate && r.EndDate >= maintenanceForCreation.StartDate && r.Status != "Canceled"))
                throw new RoomIsReservedException(roomId);

            var maintenanceEntity = _mapper.Map<Maintenance>(maintenanceForCreation);

            _repository.Maintenance.CreateMaintenanceForRoom(roomId, maintenanceEntity);
            await _repository.SaveAsync();
            
            var maintenanceToReturn = _mapper.Map<MaintenanceDto>(maintenanceEntity);
            
            return maintenanceToReturn;
        }
        
        public async Task DeleteMaintenanceForRoomAsync(Guid roomId, Guid id, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);

            var maintenanceForRoom = await _repository.Maintenance.GetMaintenanceAsync(roomId, id, trackChanges);
            if (maintenanceForRoom is null)
                throw new MaintenanceNotFoundException(id);

            _repository.Maintenance.DeleteMaintenance(maintenanceForRoom);
            await _repository.SaveAsync();
        }
        
        public async Task UpdateMaintenanceForRoomAsync(Guid roomId, Guid id, MaintenanceForUpdateDto maintenanceForUpdate, bool roomTrackChanges, bool mainTrackChanges)
        {
            var room = await _repository.Room.GetRoomWithDetailsAsync(roomId, roomTrackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);

            if (room.Reservations.Any(r => r.StartDate <= maintenanceForUpdate.EndDate && r.EndDate >= maintenanceForUpdate.StartDate && r.Status != "Canceled"))
                throw new RoomIsReservedException(roomId);
            
            var maintenanceEntity = await _repository.Maintenance.GetMaintenanceAsync(roomId, id, mainTrackChanges);
            if (maintenanceEntity is null)
                throw new MaintenanceNotFoundException(id);

            _mapper.Map(maintenanceForUpdate, maintenanceEntity);
            await _repository.SaveAsync();
        }
    }
}
