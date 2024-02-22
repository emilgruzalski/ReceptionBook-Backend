using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers(bool trackChanges);
        Customer GetCustomer(Guid customerId, bool trackChanges);
    }
}
