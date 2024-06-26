﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
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
                .Search(customerParameters.SearchTerm)
                .Sort(customerParameters.OrderBy)
                .Skip((customerParameters.PageNumber - 1) * customerParameters.PageSize)
                .Take(customerParameters.PageSize)
                .ToListAsync();
            
            var count = await FindAll(trackChanges).CountAsync();
            
            return new PagedList<Customer>(customers, count, customerParameters.PageNumber, customerParameters.PageSize);
        }

        public async Task<Customer> GetCustomerWithDetailsAsync(Guid customerId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(customerId), trackChanges)
                .Include(c => c.Reservations)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Customer>> GetCustomersIdsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();
        
        public void CreateCustomer(Customer customer) => Create(customer);
        
        public async Task<IEnumerable<Customer>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        
        public void DeleteCustomer(Customer customer) => Delete(customer);

        public Task<bool> CustomerEmailExistsAsync(string customerEmail) =>
            FindByCondition(c => c.Email.Equals(customerEmail), false)
                .AnyAsync();

        public Task<bool> CustomerEmailExistsAsync(Guid id, string customerEmail) =>
            FindByCondition(c => c.Email.Equals(customerEmail) && !c.Id.Equals(id), false)
                .AnyAsync();
    }
}
