using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<PagedList<Customer>> GetAllCustomersAsync(bool trackChanges, CustomerParameters customerParameters);
        Task<Customer> GetCustomerWithDetailsAsync(Guid customerId, bool trackChanges);
        Task<IEnumerable<Customer>> GetCustomersIdsAsync(bool trackChanges);
        void CreateCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCustomer(Customer customer);
    }
}
