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
    public static class RepositoryRoomExtensions
    {
        public static IQueryable<Room> Search(this IQueryable<Room> rooms, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return rooms;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return rooms.Where(r => r.Number.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Room> FilterRooms(this IQueryable<Room> rooms, string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return rooms;

            return rooms.Where(r => r.Type.ToLower().Equals(type.ToLower()));
        }

        public static IQueryable<Room> Sort(this IQueryable<Room> rooms, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return rooms.OrderBy(c => c.Number);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Room).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return rooms.OrderBy(e => e.Number);

            return rooms.OrderBy(orderQuery);
        }
    }
}
