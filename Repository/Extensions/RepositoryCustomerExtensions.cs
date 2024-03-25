using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryCustomerExtensions
    {
        public static IQueryable<Customer> Search(this IQueryable<Customer> customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return customers;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return customers.Where(c => c.FirstName.ToLower().Contains(lowerCaseTerm) ||
                                        c.LastName.ToLower().Contains(lowerCaseTerm) ||
                                        c.Email.ToLower().Contains(lowerCaseTerm) ||
                                        c.PhoneNumber.ToLower().Contains(lowerCaseTerm));
        }
    }
}
