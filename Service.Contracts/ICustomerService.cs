﻿using System;
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
    }
}