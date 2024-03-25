using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static IQueryable<Room> FilterByRoomType(this IQueryable<Room> rooms, string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return rooms;

            return rooms.Where(r => r.Type.ToLower().Equals(type.ToLower()));
        }
    }
}
