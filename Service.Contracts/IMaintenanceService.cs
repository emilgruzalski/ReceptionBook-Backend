﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IMaintenanceService
    {
        IEnumerable<MaintenanceDto> GetMaintenances(Guid roomId, bool trackChanges);
        MaintenanceDto GetMaintenance(Guid roomId, Guid Id, bool trackChanges);
        MaintenanceDto CreateMaintenanceForRoom(Guid roomId, MaintenanceForCreationDto maintenance, bool trackChanges);
        void DeleteMaintenanceForRoom(Guid roomId, Guid id, bool trackChanges);
        void UpdateMaintenanceForRoom(Guid roomId, Guid id, MaintenanceForUpdateDto maintenance, bool roomTrackChanges, bool mainTrackChanges);
    }
}
