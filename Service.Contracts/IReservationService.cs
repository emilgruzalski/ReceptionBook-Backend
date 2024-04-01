using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IReservationService
    {
        Task<(IEnumerable<ReservationDto> reservations, MetaData metaData)> GetAllReservationsAsync(bool trackChanges, ReservationParameters reservationParameters);
        Task<ReservationWithDetailsDto> GetReservationAsync(Guid Id, bool trackChanges);
        Task<ReservationDto> CreateReservationAsync(ReservationForCreationDto reservation);
        Task DeleteReservationAsync(Guid Id, bool trackChanges);
        Task UpdateReservationAsync(Guid Id, ReservationForUpdateDto reservation, bool trackChanges);
    }
}
