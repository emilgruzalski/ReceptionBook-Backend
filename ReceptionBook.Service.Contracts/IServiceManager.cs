using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionBook.Service.Contracts
{
    public interface IServiceManager
    {
        IMaintenanceService MaintenanceService { get; }
        IReservationService ReservationService { get; }
        ICustomerService CustomerService { get; }
        IRoomService RoomService { get; }
    }
}
