namespace Shared.DataTransferObjects;

[Serializable]
public record ReservationForCustomerDto(Guid Id, DateTime StartDate, DateTime EndDate, string RoomNumber, string Status, Decimal TotalPrice);