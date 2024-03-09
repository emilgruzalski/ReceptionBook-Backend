using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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
        
        public CustomerDto CreateCustomer(CustomerForCreationDto customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            
            _repository.Customer.CreateCustomer(customerEntity);
            _repository.Save();
            
            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            
            return customerToReturn;
        }

        public IEnumerable<CustomerDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            
            var customersEntities = _repository.Customer.GetByIds(ids, trackChanges);
            if (ids.Count() != customersEntities.Count())
                throw new CollectionByIdsBadRequestException();
            
            var customersToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersEntities);
            
            return customersToReturn;
        }
        
        public (IEnumerable<CustomerDto> customers, string ids) CreateCustomerCollection(IEnumerable<CustomerForCreationDto> customerCollection)
        {
            if (customerCollection is null)
                throw new CustomerCollectionBadRequest();
            
            var customersEntities = _mapper.Map<IEnumerable<Customer>>(customerCollection);
            foreach (var customer in customersEntities)
            {
                _repository.Customer.CreateCustomer(customer);
            }

            _repository.Save();
            
            var customerCollectionToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersEntities);
            var ids = string.Join(",", customerCollectionToReturn.Select(c => c.Id));
            
            return (customers: customerCollectionToReturn, ids: ids);
        }
        
        public void DeleteCustomer(Guid customerId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            _repository.Customer.DeleteCustomer(customer);
            _repository.Save();
        }
    }
}
