namespace Entities.Exceptions;

public sealed class RoomAlreadyReservedException : BadRequestException
{
    public RoomAlreadyReservedException(Guid roomId, DateOnly startDate, DateOnly endDate)
        : base($"Room with id {roomId} is already reserved from {startDate} to {endDate}.")
    {
    }
}