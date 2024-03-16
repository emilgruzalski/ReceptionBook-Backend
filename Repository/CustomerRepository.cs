using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Customer>> GetAllCustomersAsync(bool trackChanges,
            CustomerParameters customerParameters)
        {
            var customers = await FindAll(trackChanges)
                .OrderBy(c => c.FirstName)
                .Skip((customerParameters.PageNumber - 1) * customerParameters.PageSize)
                .Take(customerParameters.PageSize)
                .ToListAsync();
            
            var count = await FindAll(trackChanges).CountAsync();
            
            return new PagedList<Customer>(customers, count, customerParameters.PageNumber, customerParameters.PageSize);
        }

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
