using Microsoft.AspNetCore.Mvc;
using ReceptionBook.Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ReceptionBook.Presentation.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public CustomersController(IServiceManager service) => _service = service;
    
    [HttpGet]
    public IActionResult GetCustomers()
    {
        var customers = _service.CustomerService.GetAllCustomers(trackChanges: false);
        
        return Ok(customers);
    }
    
    [HttpGet("{id:guid}", Name = "CustomerById")]
    public IActionResult GetCustomer(Guid id)
    {
        var customer = _service.CustomerService.GetCustomer(id, trackChanges: false);
        
        return Ok(customer);
    }
    
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CustomerForCreationDto customer)
    {
        if (customer is null)
            return BadRequest("CustomerForCreationDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var createdCustomer = _service.CustomerService.CreateCustomer(customer);
        
        return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
    }
    
    [HttpGet("collection/({ids})", Name = "CustomersCollection")]
    public IActionResult GetCustomersCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var customers = _service.CustomerService.GetByIds(ids, trackChanges: false);
        
        return Ok(customers);
    }
    
    [HttpPost("collection")]
    public IActionResult CreateCustomerCollection([FromBody] IEnumerable<CustomerForCreationDto> customerCollection)
    {
        var result = _service.CustomerService.CreateCustomerCollection(customerCollection);
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        return CreatedAtRoute("CustomerCollection", new { result.ids }, result.customers);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCustomer(Guid id)
    {
        _service.CustomerService.DeleteCustomer(id, trackChanges: false);
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCustomer(Guid id, [FromBody] CustomerForUpdateDto customer)
    {
        if (customer is null)
            return BadRequest("CustomerForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        _service.CustomerService.UpdateCustomer(id, customer, trackChanges: true);
        
        return NoContent();
    }
    
    [HttpGet("{customerId}/reservations")]
    public IActionResult GetReservationsForCustomer(Guid customerId)
    {
        var reservations = _service.ReservationService.GetReservationsForCustomer(customerId, trackChanges: false);
        
        return Ok(reservations);
    }
}