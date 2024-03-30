using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

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

        public static IQueryable<Maintenance> Sort(this IQueryable<Maintenance> maintenances, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return maintenances.OrderBy(c => c.StartDate);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Maintenance).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return maintenances.OrderBy(e => e.StartDate);

            return maintenances.OrderBy(orderQuery);
        }
    }
}
