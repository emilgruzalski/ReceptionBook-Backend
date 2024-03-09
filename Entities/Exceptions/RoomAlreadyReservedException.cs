namespace Entities.Exceptions;

public sealed class RoomAlreadyReservedException : BadRequestException
{
    public RoomAlreadyReservedException(Guid roomId, DateTime startDate, DateTime endDate)
        : base($"Room with id {roomId} is already reserved from {startDate} to {endDate}.")
    {
    }
}