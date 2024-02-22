using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Repository
{
    internal sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }
        
        public IEnumerable<Customer> GetAllCustomers(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.FirstName)
                .ToList();
        
        public Customer GetCustomer(Guid customerId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(customerId), trackChanges)
                .SingleOrDefault();
    }
}
