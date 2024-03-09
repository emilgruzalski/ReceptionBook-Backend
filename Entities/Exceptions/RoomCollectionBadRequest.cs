namespace Entities.Exceptions;

public sealed class RoomCollectionBadRequest : BadRequestException
{
    public RoomCollectionBadRequest()
        : base("Room collection sent from a client is null.")
    {
    }
}