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
        IEnumerable<CustomerDto> GetAllCustomers(bool trackChanges);
        CustomerDto GetCustomer(Guid customerId, bool trackChanges);
        CustomerDto CreateCustomer(CustomerForCreationDto customer);
        IEnumerable<CustomerDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        (IEnumerable<CustomerDto> customers, string ids) CreateCustomerCollection(IEnumerable<CustomerForCreationDto> customerCollection);
        void DeleteCustomer(Guid customerId, bool trackChanges);
    }
}
