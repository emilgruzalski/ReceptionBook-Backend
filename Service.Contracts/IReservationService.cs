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
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges);
        Task<ReservationDto> GetReservationAsync(Guid Id, bool trackChanges);
        Task<IEnumerable<ReservationDto>> GetReservationsForRoomAsync(Guid roomId, bool trackChanges);
        Task<IEnumerable<ReservationDto>> GetReservationsForCustomerAsync(Guid customerId, bool trackChanges);
        Task<ReservationDto> CreateReservationAsync(ReservationForCreationDto reservation);
        Task DeleteReservationAsync(Guid Id, bool trackChanges);
        Task UpdateReservationAsync(Guid Id, ReservationForUpdateDto reservation, bool trackChanges);
    }
}
