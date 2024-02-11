using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ReceptionBook.Contracts;
using ReceptionBook.Service.Contracts;

namespace ReceptionBook.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IReservationService> _reservationService;
        private readonly Lazy<IMaintenanceService> _maintenanceService;
        private readonly Lazy<IRoomService> _roomService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, logger, mapper));
            _reservationService = new Lazy<IReservationService>(() => new ReservationService(repositoryManager, logger, mapper));
            _maintenanceService = new Lazy<IMaintenanceService>(() => new MaintenanceService(repositoryManager, logger, mapper));
            _roomService = new Lazy<IRoomService>(() => new RoomService(repositoryManager, logger, mapper));
        }

        public ICustomerService CustomerService => _customerService.Value;
        public IReservationService ReservationService => _reservationService.Value;
        public IMaintenanceService MaintenanceService => _maintenanceService.Value;
        public IRoomService RoomService => _roomService.Value;
    }
}
