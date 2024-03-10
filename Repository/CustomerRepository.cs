using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }
        
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.FirstName)
                .ToListAsync();
        
        public async Task<Customer> GetCustomerAsync(Guid customerId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(customerId), trackChanges)
                .SingleOrDefaultAsync();
        
        public void CreateCustomer(Customer customer) => Create(customer);
        
        public async Task<IEnumerable<Customer>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        
        public void DeleteCustomer(Customer customer) => Delete(customer);
    }
}
