using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReceptionBook.Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace ReceptionBook.Presentation.Controllers;

[Route("api/customers")]
[Authorize(Roles = "Administrator, Manager")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public CustomersController(IServiceManager service) => _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] CustomerParameters customerParameters)
    {
        var pagedResult = await _service.CustomerService.GetAllCustomersAsync(trackChanges: false, customerParameters);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.customers);
    }
    
    [HttpGet("{id:guid}", Name = "CustomerById")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var customer = await _service.CustomerService.GetCustomerAsync(id, trackChanges: false);
        
        return Ok(customer);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customer)
    {
        if (customer is null)
            return BadRequest("CustomerForCreationDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var createdCustomer = await _service.CustomerService.CreateCustomerAsync(customer);
        
        return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
    }
    
    [HttpGet("collection/({ids})", Name = "CustomersCollection")]
    public async Task<IActionResult> GetCustomersCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var customers = await _service.CustomerService.GetByIdsAsync(ids, trackChanges: false);
        
        return Ok(customers);
    }
    
    [HttpPost("collection")]
    public async Task<IActionResult> CreateCustomerCollection([FromBody] IEnumerable<CustomerForCreationDto> customerCollection)
    {
        var result = await _service.CustomerService.CreateCustomerCollectionAsync(customerCollection);
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        return CreatedAtRoute("CustomersCollection", new { result.ids }, result.customers);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await _service.CustomerService.DeleteCustomerAsync(id, trackChanges: false);
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerForUpdateDto customer)
    {
        if (customer is null)
            return BadRequest("CustomerForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        await _service.CustomerService.UpdateCustomerAsync(id, customer, trackChanges: true);
        
        return NoContent();
    }
}