namespace Shared.DataTransferObjects;

[Serializable]
public record ReservationForRoomDto(Guid Id, DateTime StartDate, DateTime EndDate, string CustomerName, string Status, Decimal TotalPrice);