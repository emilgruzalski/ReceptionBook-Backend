﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

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

        public static IQueryable<Reservation> FilterReservations(this IQueryable<Reservation> reservations, string? Status)
        {
            if (string.IsNullOrWhiteSpace(Status))
                return reservations;

            return reservations.Where(r => r.Status.ToLower().Equals(Status.ToLower()));
        }

        public static IQueryable<Reservation> Sort(this IQueryable<Reservation> reservations, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return reservations.OrderBy(c => c.StartDate);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Reservation).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return reservations.OrderBy(e => e.StartDate);

            return reservations.OrderBy(orderQuery);
        }
    }
}
