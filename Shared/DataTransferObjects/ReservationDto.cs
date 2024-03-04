﻿namespace Shared.DataTransferObjects;

public record ReservationDto(Guid Id, DateTime StartDate, DateTime EndDate, string Status, Decimal TotalPrice, string CustomerName, string RoomNumber);