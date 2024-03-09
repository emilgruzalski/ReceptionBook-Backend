namespace Entities.Exceptions;

public class RoomIsReservedException : BadRequestException
{
    public RoomIsReservedException(Guid roomId)
        : base($"Room with id {roomId} is reserved for the selected period.")
    {
    }
}