using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IReservationService
    {
        IEnumerable<ReservationDto> GetAllReservations(bool trackChanges);
        ReservationDto GetReservation(Guid Id, bool trackChanges);
        IEnumerable<ReservationForRoomDto> GetReservationsForRoom(Guid roomId, bool trackChanges);
        ReservationForRoomDto GetReservationForRoom(Guid roomId, Guid Id, bool trackChanges);
        IEnumerable<ReservationForCustomerDto> GetReservationsForCustomer(Guid customerId, bool trackChanges);
        ReservationForCustomerDto GetReservationForCustomer(Guid customerId, Guid Id, bool trackChanges);
    }
}
