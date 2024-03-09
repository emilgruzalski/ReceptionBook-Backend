﻿namespace Shared.DataTransferObjects;

public record ReservationForCreationDto(DateTime StartDate, DateTime EndDate, string Status, Decimal TotalPrice, Guid CustomerId, Guid RoomId);