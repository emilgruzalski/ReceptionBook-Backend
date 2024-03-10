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
        Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool trackChanges);
        Task<Reservation> GetReservationAsync(Guid reservationId, bool trackChanges);
        Task<IEnumerable<Reservation>> GetReservationsForRoomAsync(Guid roomId, bool trackChanges);
        Task<IEnumerable<Reservation>> GetReservationsForCustomerAsync(Guid customerId, bool trackChanges);
        void CreateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
    }
}
