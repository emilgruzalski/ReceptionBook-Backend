namespace Shared.DataTransferObjects;

public record ReservationForRoomDto(Guid Id, DateTime StartDate, DateTime EndDate, string CustomerName, string Status, Decimal TotalPrice);