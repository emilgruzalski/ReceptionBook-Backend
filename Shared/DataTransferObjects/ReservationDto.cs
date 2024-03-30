namespace Shared.DataTransferObjects;

[Serializable]
public record ReservationDto(Guid Id, DateOnly StartDate, DateOnly EndDate, string Status, Decimal TotalPrice, string CustomerName, string RoomNumber);