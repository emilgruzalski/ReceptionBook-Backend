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
                .Search(reservationParameters.SearchTerm)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .Sort(reservationParameters.OrderBy)
                .Skip((reservationParameters.PageNumber - 1) * reservationParameters.PageSize)
                .Take(reservationParameters.PageSize)
                .ToListAsync();
                
            var count = await FindAll(trackChanges).Search(reservationParameters.SearchTerm).CountAsync();
            
            return new PagedList<Reservation>(reservations, count, reservationParameters.PageNumber, reservationParameters.PageSize);
        }

        public async Task<Reservation> GetReservationWithDetailsAsync(Guid reservationId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(reservationId), trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .SingleOrDefaultAsync();

        public void CreateReservation(Reservation reservation) => Create(reservation);
        
        public void DeleteReservation(Reservation reservation) => Delete(reservation);

        public async Task<IEnumerable<Reservation>> GetAllReservationsForRaportAsync(bool trackChanges) =>
            await FindByCondition(r => r.Status != "Cancelled", trackChanges)
                .ToListAsync();
    }
}
