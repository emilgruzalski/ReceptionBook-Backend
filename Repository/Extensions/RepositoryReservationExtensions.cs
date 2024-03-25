using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryReservationExtensions
    {
        public static IQueryable<Reservation> Search(this IQueryable<Reservation> reservations, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return reservations;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return reservations.Where(r => r.Customer.FirstName.ToLower().Contains(lowerCaseTerm) ||
                                           r.Customer.LastName.ToLower().Contains(lowerCaseTerm) ||
                                           r.Room.Number.ToLower().Contains(lowerCaseTerm));
                                                                                              
        }

        public static IQueryable<Reservation> FilterByStatus(this IQueryable<Reservation> reservations, string? Status)
        {
            if (string.IsNullOrWhiteSpace(Status))
                return reservations;

            return reservations.Where(r => r.Status.ToLower().Equals(Status.ToLower()));
        }
    }
}
