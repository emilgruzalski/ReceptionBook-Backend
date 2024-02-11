﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionBook.Contracts
{
    public interface IRepositoryManager
    {
        IMaintenanceRepository Maintenance { get; }
        IReservationRepository Reservation { get; }
        ICustomerRepository Customer { get; }
        IRoomRepository Room { get; }
        void Save();
    }
}