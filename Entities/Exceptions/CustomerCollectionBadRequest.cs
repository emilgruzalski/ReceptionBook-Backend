namespace Entities.Exceptions;

public sealed class CustomerCollectionBadRequest : BadRequestException
{
    public CustomerCollectionBadRequest()
        : base("Customer collection sent from a client is null.")
    {
    }
}