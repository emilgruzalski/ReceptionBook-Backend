namespace Entities.Exceptions;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException(Guid customerId) 
        : base($"The customer with id {customerId} doesn't exist in the database.")
    {
    }
}