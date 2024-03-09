namespace Shared.DataTransferObjects;

public record RoomForCreationDto(string Number, string Type, decimal Price, IEnumerable<MaintenanceForCreationDto> Maintenances);