namespace Shared.DataTransferObjects;

[Serializable]
public record MaintenanceDto(Guid Id, string Description, DateTime StartDate, DateTime EndDate, Decimal Cost);