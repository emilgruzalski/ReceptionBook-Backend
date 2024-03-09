namespace Shared.DataTransferObjects;

public record RoomForUpdateDto(string Number, string Type, decimal Price, IEnumerable<MaintenanceForCreationDto> Maintenances);