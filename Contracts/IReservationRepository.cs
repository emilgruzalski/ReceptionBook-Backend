using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAllReservations(bool trackChanges);
        Reservation GetReservation(Guid reservationId, bool trackChanges);
        IEnumerable<Reservation> GetReservationsForRoom(Guid roomId, bool trackChanges);
        Reservation GetReservationForRoom(Guid roomId, Guid reservationId, bool trackChanges);
        IEnumerable<Reservation> GetReservationsForCustomer(Guid customerId, bool trackChanges);
        Reservation GetReservationForCustomer(Guid customerId, Guid reservationId, bool trackChanges);
        void CreateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
    }
}
