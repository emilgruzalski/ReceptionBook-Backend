namespace Shared.DataTransferObjects;

public record ReservationForUpdateDto(DateTime StartDate, DateTime EndDate, string Status, Decimal TotalPrice, Guid CustomerId, Guid RoomId);