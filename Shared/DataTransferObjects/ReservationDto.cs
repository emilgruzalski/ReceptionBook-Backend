namespace Shared.DataTransferObjects;

[Serializable]
public record ReservationDto(Guid Id, DateOnly StartDate, DateOnly EndDate, Decimal TotalPrice, string Status, string CustomerName, string RoomNumber);