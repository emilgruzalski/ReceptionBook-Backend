using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public IEnumerable<CustomerDto> GetAllCustomers(bool trackChanges)
        {
            var customers = _repository.Customer.GetAllCustomers(trackChanges);
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
        
        public CustomerDto GetCustomer(Guid customerId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
