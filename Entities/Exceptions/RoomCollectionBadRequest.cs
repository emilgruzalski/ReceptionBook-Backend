namespace Entities.Exceptions;

public sealed class RoomCollectionBadRequestException : BadRequestException
{
    public RoomCollectionBadRequestException()
        : base("Room collection sent from a client is null.")
    {
    }
}