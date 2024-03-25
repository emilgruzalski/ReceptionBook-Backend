using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Reservation>> GetAllReservationsAsync(bool trackChanges,
            ReservationParameters reservationParameters)
        {
            var reservations = await FindAll(trackChanges)
                .FilterByStatus(reservationParameters.Status)
                .Search(reservationParameters.SearchTerm)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .OrderBy(r => r.StartDate)
                .Skip((reservationParameters.PageNumber - 1) * reservationParameters.PageSize)
                .Take(reservationParameters.PageSize)
                .ToListAsync();
                
            var count = await FindAll(trackChanges).FilterByStatus(reservationParameters.Status).Search(reservationParameters.SearchTerm).CountAsync();
            
            return new PagedList<Reservation>(reservations, count, reservationParameters.PageNumber, reservationParameters.PageSize);
        }

        public async Task<Reservation> GetReservationAsync(Guid reservationId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(reservationId), trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .SingleOrDefaultAsync();

        public async Task<PagedList<Reservation>> GetReservationsForRoomAsync(Guid roomId,
            ReservationParameters reservationParameters, bool trackChanges)
        {
            var reservations = await FindByCondition(r => r.RoomId.Equals(roomId), trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .OrderBy(r => r.StartDate)
                .Skip((reservationParameters.PageNumber - 1) * reservationParameters.PageSize)
                .Take(reservationParameters.PageSize)
                .ToListAsync();
            
            var count = await FindByCondition(r => r.RoomId.Equals(roomId), trackChanges).CountAsync();
            
            return new PagedList<Reservation>(reservations, count, reservationParameters.PageNumber, reservationParameters.PageSize);
        }

        public async Task<PagedList<Reservation>> GetReservationsForCustomerAsync(Guid customerId,
            ReservationParameters reservationParameters, bool trackChanges)
        {
            var reservations = await FindByCondition(r => r.CustomerId.Equals(customerId), trackChanges)
                .Include(r => r.Room)
                .Include(r => r.Customer)
                .OrderBy(r => r.StartDate)
                .Skip((reservationParameters.PageNumber - 1) * reservationParameters.PageSize)
                .Take(reservationParameters.PageSize)
                .ToListAsync();
            
            var count = await FindByCondition(r => r.CustomerId.Equals(customerId), trackChanges).CountAsync();
            
            return new PagedList<Reservation>(reservations, count, reservationParameters.PageNumber, reservationParameters.PageSize);
        }

        public void CreateReservation(Reservation reservation) => Create(reservation);
        
        public void DeleteReservation(Reservation reservation) => Delete(reservation);
    }
}
