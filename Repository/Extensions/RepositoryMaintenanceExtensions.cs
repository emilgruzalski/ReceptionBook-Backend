using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryMaintenanceExtensions
    {
        public static IQueryable<Maintenance> Search(this IQueryable<Maintenance> maintenances, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return maintenances;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return maintenances.Where(m => m.Description.ToLower().Contains(lowerCaseTerm));
        }
    }
}
