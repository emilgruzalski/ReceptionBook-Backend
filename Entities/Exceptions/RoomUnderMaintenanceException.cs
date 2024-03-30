namespace Entities.Exceptions;

public sealed class RoomUnderMaintenanceException : BadRequestException
{
    public RoomUnderMaintenanceException(Guid roomId, DateOnly startDate, DateOnly endDate)
        : base($"Room with id {roomId} is under maintenance from {startDate} to {endDate}.")
    {
    }
}