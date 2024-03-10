using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .OrderBy(r => r.StartDate)
                .ToListAsync();
        
        public async Task<Reservation> GetReservationAsync(Guid reservationId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(reservationId), trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .SingleOrDefaultAsync();
        
        public async Task<IEnumerable<Reservation>> GetReservationsForRoomAsync(Guid roomId, bool trackChanges) =>
            await FindByCondition(r => r.RoomId.Equals(roomId), trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .OrderBy(r => r.StartDate)
                .ToListAsync();
        
        public async Task<IEnumerable<Reservation>> GetReservationsForCustomerAsync(Guid customerId, bool trackChanges) =>
            await FindByCondition(r => r.CustomerId.Equals(customerId), trackChanges)
                .Include(r => r.Room)
                .Include(r => r.Customer)
                .OrderBy(r => r.StartDate)
                .ToListAsync();
        
        public void CreateReservation(Reservation reservation) => Create(reservation);
        
        public void DeleteReservation(Reservation reservation) => Delete(reservation);
    }
}
