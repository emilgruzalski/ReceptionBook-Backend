namespace Entities.Exceptions;

public sealed class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(Guid roomId)
        : base($"The room with id {roomId} doesn't exist in the database.")
    {
    }
}