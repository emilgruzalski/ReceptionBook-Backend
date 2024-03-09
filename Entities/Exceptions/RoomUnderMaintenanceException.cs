namespace Entities.Exceptions;

public sealed class RoomUnderMaintenanceException : BadRequestException
{
    public RoomUnderMaintenanceException(Guid roomId, DateTime startDate, DateTime endDate)
        : base($"Room with id {roomId} is under maintenance from {startDate} to {endDate}.")
    {
    }
}