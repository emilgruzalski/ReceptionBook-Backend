namespace Shared.DataTransferObjects;

[Serializable]
public record MaintenanceDto(Guid Id, string Description, DateOnly StartDate, DateOnly EndDate, Decimal Cost);