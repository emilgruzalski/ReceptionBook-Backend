﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IReservationRepository Reservation { get; }
        ICustomerRepository Customer { get; }
        IRoomRepository Room { get; }
        Task SaveAsync();
    }
}
