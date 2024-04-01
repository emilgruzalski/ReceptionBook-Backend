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
        Task<Reservation> GetReservationWithDetailsAsync(Guid reservationId, bool trackChanges);
        void CreateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
    }
}
