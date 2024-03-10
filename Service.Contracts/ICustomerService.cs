using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(bool trackChanges);
        Task<CustomerDto> GetCustomerAsync(Guid customerId, bool trackChanges);
        Task<CustomerDto> CreateCustomerAsync(CustomerForCreationDto customer);
        Task<IEnumerable<CustomerDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CustomerDto> customers, string ids)> CreateCustomerCollectionAsync(IEnumerable<CustomerForCreationDto> customerCollection);
        Task DeleteCustomerAsync(Guid customerId, bool trackChanges);
        Task UpdateCustomerAsync(Guid customerId, CustomerForUpdateDto customer, bool trackChanges);
    }
}
