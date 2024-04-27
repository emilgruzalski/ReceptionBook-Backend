namespace Entities.Exceptions;

public class RoomIsReservedException : BadRequestException
{
    public RoomIsReservedException(string roomId)
        : base($"Room with number {roomId} is reserved for the selected period.")
    {
    }
}