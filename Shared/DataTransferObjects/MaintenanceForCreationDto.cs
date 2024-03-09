namespace Shared.DataTransferObjects;

public record MaintenanceForCreationDto(string Description, DateTime StartDate, DateTime EndDate, Decimal Cost);