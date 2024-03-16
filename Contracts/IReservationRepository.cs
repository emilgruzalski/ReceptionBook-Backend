using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IReservationRepository
    {
        Task<PagedList<Reservation>> GetAllReservationsAsync(bool trackChanges, ReservationParameters reservationParameters);
        Task<Reservation> GetReservationAsync(Guid reservationId, bool trackChanges);
        Task<PagedList<Reservation>> GetReservationsForRoomAsync(Guid roomId, ReservationParameters reservationParameters, bool trackChanges);
        Task<PagedList<Reservation>> GetReservationsForCustomerAsync(Guid customerId, ReservationParameters reservationParameters, bool trackChanges);
        void CreateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
    }
}
