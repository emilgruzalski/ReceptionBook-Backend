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
        IEnumerable<ReservationDto> GetReservationsForRoom(Guid roomId, bool trackChanges);
        ReservationDto GetReservationForRoom(Guid roomId, Guid Id, bool trackChanges);
        IEnumerable<ReservationDto> GetReservationsForCustomer(Guid customerId, bool trackChanges);
        ReservationDto GetReservationForCustomer(Guid customerId, Guid Id, bool trackChanges);
        ReservationDto CreateReservation(ReservationForCreationDto reservation);
        void DeleteReservation(Guid Id, bool trackChanges);
        void UpdateReservation(Guid Id, ReservationForUpdateDto reservation, bool trackChanges);
    }
}
