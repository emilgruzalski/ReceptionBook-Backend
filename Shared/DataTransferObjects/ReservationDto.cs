namespace Shared.DataTransferObjects;

public record ReservationDto(Guid Id, DateTime StartDate, DateTime EndDate, string Status, Decimal TotalCost, string CustomerName, string RoomNumber);