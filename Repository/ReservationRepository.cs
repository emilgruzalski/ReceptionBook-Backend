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
        
        public IEnumerable<Reservation> GetAllReservations(bool trackChanges) =>
            FindAll(trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .OrderBy(r => r.StartDate)
                .ToList();
        
        public Reservation GetReservation(Guid reservationId, bool trackChanges) =>
            FindByCondition(r => r.Id.Equals(reservationId), trackChanges)
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .SingleOrDefault();
        
        public IEnumerable<Reservation> GetReservationsForRoom(Guid roomId, bool trackChanges) =>
            FindByCondition(r => r.RoomId.Equals(roomId), trackChanges)
                .Include(r => r.Customer)
                .OrderBy(r => r.StartDate)
                .ToList();
        
        public Reservation GetReservationForRoom(Guid roomId, Guid reservationId, bool trackChanges) =>
            FindByCondition(r => r.RoomId.Equals(roomId) && r.Id.Equals(reservationId), trackChanges)
                .Include(r => r.Customer)
                .SingleOrDefault();
        
        public IEnumerable<Reservation> GetReservationsForCustomer(Guid customerId, bool trackChanges) =>
            FindByCondition(r => r.CustomerId.Equals(customerId), trackChanges)
                .Include(r => r.Room)
                .OrderBy(r => r.StartDate)
                .ToList();
        
        public Reservation GetReservationForCustomer(Guid customerId, Guid reservationId, bool trackChanges) =>
            FindByCondition(r => r.CustomerId.Equals(customerId) && r.Id.Equals(reservationId), trackChanges)
                .Include(r => r.Room)
                .SingleOrDefault();
        
        public void CreateReservation(Reservation reservation) => Create(reservation);
    }
}
