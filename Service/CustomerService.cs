using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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
        
        public async Task<(IEnumerable<CustomerDto> customers, MetaData metaData)> GetAllCustomersAsync(bool trackChanges, CustomerParameters customerParameters)
        {
            var customersWithMetaData = await _repository.Customer.GetAllCustomersAsync(trackChanges, customerParameters);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);

            return (customers: customersDto, metaData: customersWithMetaData.MetaData);
        }
        
        public async Task<CustomerWithDetalisDto> GetCustomerAsync(Guid customerId, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerWithDetailsAsync(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            return _mapper.Map<CustomerWithDetalisDto>(customer);
        }
        
        public async Task<CustomerDto> CreateCustomerAsync(CustomerForCreationDto customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            
            _repository.Customer.CreateCustomer(customerEntity);
            await _repository.SaveAsync();
            
            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            
            return customerToReturn;
        }

        public async Task<IEnumerable<CustomerDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            
            var customersEntities = await _repository.Customer.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != customersEntities.Count())
                throw new CollectionByIdsBadRequestException();
            
            var customersToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersEntities);
            
            return customersToReturn;
        }
        
        public async Task<(IEnumerable<CustomerDto> customers, string ids)> CreateCustomerCollectionAsync(IEnumerable<CustomerForCreationDto> customerCollection)
        {
            if (customerCollection is null)
                throw new CustomerCollectionBadRequest();
            
            var customersEntities = _mapper.Map<IEnumerable<Customer>>(customerCollection);
            foreach (var customer in customersEntities)
            {
                _repository.Customer.CreateCustomer(customer);
            }

            await _repository.SaveAsync();
            
            var customerCollectionToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersEntities);
            var ids = string.Join(",", customerCollectionToReturn.Select(c => c.Id));
            
            return (customers: customerCollectionToReturn, ids: ids);
        }
        
        public async Task DeleteCustomerAsync(Guid customerId, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerWithDetailsAsync(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            _repository.Customer.DeleteCustomer(customer);
            await _repository.SaveAsync();
        }

        public async Task UpdateCustomerAsync(Guid customerId, CustomerForUpdateDto customer, bool trackChanges)
        {
            var customerEntity = await _repository.Customer.GetCustomerWithDetailsAsync(customerId, trackChanges);
            if (customerEntity is null)
                throw new CustomerNotFoundException(customerId);

            _mapper.Map(customer, customerEntity);
            await _repository.SaveAsync();
        }
    }
}
